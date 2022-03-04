//references       
const addButton = document.querySelector('#equipo-mantenimiento-button-add');
const upsertModal = document.querySelector('#equipo-mantenimiento-modal-upsert');
const upsertForm = document.querySelector('#equipo-mantenimiento-form-upsert');
const zinggrid = document.querySelector('zing-grid');

const selectConfig = {
    url: '/datasets/countries.json',
    selectId: '#equipo-mantenimiento-form-upsert #Pais',
    value: 'code',
    label: 'name'
};

const upsertConfig = {
    url: '/api/equipo',
    headers: { 'Content-Type': 'application/json' }
};

const upsertToggleConfig = {
    url: '/api/equipo/status',
    headers: { 'Content-Type': 'application/json' }
};

const confirmConfig = {
    title: 'Eliminando equipo!',
    message: '¿Esta seguro de que desea eliminar el equipo?'
}

//function
const refreshGrid = () => zinggrid.refresh();

const fillUpsertEquipoForm = async (equipo) => {
    upsertForm.elements['Id'].value = equipo?.id ?? '0';
    upsertForm.elements['Nombre'].value = equipo?.nombre ?? '';
    await initSelect(selectConfig, equipo?.pais);
}

function checkRenderer(stubArgument, cellDOMRef, cellRef) {
    const check = cellDOMRef.querySelector('.equipo-mantenimiento-check-estado input[type=checkbox]');
    const estado = Number(check.dataset.estado);
    check.checked = (estado === 1) ? true : false;
};

//handlers
const addButtonHandler = async (e) => {
    await initSelect(selectConfig);
    Metro.dialog.open(upsertModal);
}

const editButtonHandler = async (id) => {
    const equipo = await getData(`/api/equipo/${id}`)
    await fillUpsertEquipoForm(equipo);
    Metro.dialog.open(upsertModal);
};

const deleteButtonHandler = async (id) => {
    showConfirm(confirmConfig, async () => {
        const response = await getData(`/api/equipo/${id}`, 'DELETE');

        const { hasError, errorMessage } = response

        if (hasError) {
            showToast(errorMessage, 'alert');
            return;
        }

        Metro.dialog.close($('.mantenimiento-confirm-dialog'));
        showToast('El equipo ha sido eliminado exitosamente.');
        refreshGrid();
    });
}

const checkEstadoHandler = async (id) => {
    debugger;
    const response = await upsertData(upsertToggleConfig, { id }, 'PUT');
    const { hasError, errorMessage } = response

    if (hasError) {
        showToast(errorMessage, 'alert');
        return;
    }

    showToast('Cambio de estado exitoso.');

}

const upsertModalOnCloseHandler = () => {
    refreshGrid();
    fillUpsertEquipoForm({});
}

const upsertFormSubmitHandler = async (e) => {
    e.preventDefault();
    const values = getObjectFromForm(e);
    const { Id, ...equipo } = values;

    const response = (Id === 0) ?
        await upsertData(upsertConfig, equipo) :
        await upsertData(upsertConfig, { Id, ...equipo }, 'PUT');

    const { hasError, errorMessage } = response

    if (hasError) {
        showToast(errorMessage, 'alert');
        return;
    }

    showToast('Equipo guardado exitosamente');
    Metro.dialog.close(upsertModal)
}

//listeners
addButton.addEventListener('click', addButtonHandler);
upsertForm.addEventListener('submit', upsertFormSubmitHandler)
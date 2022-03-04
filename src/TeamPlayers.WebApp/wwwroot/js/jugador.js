//references
const addButton = document.querySelector('#jugador-mantenimiento-button-add');
const upsertModal = document.querySelector('#jugador-mantenimiento-modal-upsert');
const upsertForm = document.querySelector('#jugador-mantenimiento-form-upsert');
const equipoModal = document.querySelector('#jugador-mantenimiento-modal-equipo');
const equipoForm = document.querySelector('#jugador-mantenimiento-form-equipo');
const zinggrid = document.querySelector('zing-grid');

const selectSexoConfig = {
    url: '/datasets/sexes.json',
    selectId: '#jugador-mantenimiento-form-upsert #Sexo',
    value: 'short',
    label: 'description'
};

const selectEquipoConfig = {
    url: '/api/equipo?estado=1',
    selectId: '#jugador-mantenimiento-form-equipo #IdEquipo',
    value: 'id',
    label: 'nombre'
};

const upsertConfig = {
    url: '/api/jugador',
    headers: { 'Content-Type': 'application/json' }
};

const upsertToggleConfig = {
    url: '/api/jugador/toggle',
    headers: { 'Content-Type': 'application/json' }
};

const upsertEquipoConfig = {
    url: '/api/jugador/equipo',
    headers: { 'Content-Type': 'application/json' }
};

const upsertLiberarConfig = {
    url: '/api/jugador/liberar',
    headers: { 'Content-Type': 'application/json' }
};

const confirmConfig = {
    title: 'Eliminando jugador!',
    message: '¿Esta seguro de que desea eliminar el jugador?'
}

const confirmLiberarConfig = {
    title: 'Liberar jugador!',
    message: '¿Esta seguro de que desea liberar al jugador?'
}

//function
const refreshGrid = () => zinggrid.refresh();

const fillUpsertJugadorForm = async (jugador) => {
    upsertForm.elements['Id'].value = jugador?.id ?? '0';
    upsertForm.elements['Nombre'].value = jugador?.nombre ?? '';
    upsertForm.elements['Apellido'].value = jugador?.apellido ?? '';
    upsertForm.elements['Nacimiento'].valueAsDate = new Date(jugador?.nacimiento);
    upsertForm.elements['Pasaporte'].value = jugador?.pasaporte ?? '';
    upsertForm.elements['Direccion'].value = jugador?.direccion ?? '';
    await initSelect(selectSexoConfig, jugador?.sexo);
}

const fillUpsertEquipoForm = async (jugador) => {
    equipoForm.elements['Id'].value = jugador?.id ?? '0';
    await initSelect(selectEquipoConfig, jugador?.idEquipo);
}

function checkRenderer(stubArgument, cellDOMRef, cellRef) {
    const check = cellDOMRef.querySelector('.jugador-mantenimiento-check-estado input[type=checkbox]');
    const estado = Number(check.dataset.estado);
    check.checked = (estado === 1) ? true : false;
};

//handlers
const addButtonHandler = async (e) => {
    await initSelect(selectSexoConfig);
    Metro.dialog.open(upsertModal);
}

const editButtonHandler = async (id) => {
    const jugador = await getData(`/api/jugador/${id}`)
    await fillUpsertJugadorForm(jugador);
    Metro.dialog.open(upsertModal);
};

const equipoButtonHandler = async (id) => {
    const jugador = await getData(`/api/jugador/${id}`)
    await fillUpsertEquipoForm(jugador);
    Metro.dialog.open(equipoModal);
};

const deleteButtonHandler = async (id) => {
    showConfirm(confirmConfig, async () => {
        const response = await getData(`/api/jugador/${id}`, 'DELETE');

        const { hasError, errorMessage } = response

        if (hasError) {
            showToast(errorMessage, 'alert');
            return;
        }

        Metro.dialog.close($('.mantenimiento-confirm-dialog'));
        showToast('El jugador ha sido eliminado exitosamente.');
        refreshGrid();
    });
}

const liberarButtonHandler = async (id) => {
    showConfirm(confirmLiberarConfig, async () => {
        const response = await upsertData(upsertLiberarConfig, { id, idEquipo: null }, 'PUT');

        const { hasError, errorMessage } = response

        if (hasError) {
            showToast(errorMessage, 'alert');
            return;
        }

        Metro.dialog.close($('.mantenimiento-confirm-dialog'));
        showToast('El jugador ha sido liberado exitosamente.');
        refreshGrid();
    });
}

const checkEstadoHandler = async (id) => {
    const response = await upsertData(upsertToggleConfig, { id }, 'PUT');
    const { hasError, errorMessage } = response

    if (hasError) {
        showToast(errorMessage, 'alert');
        return;
    }

    showToast('Cambio de estado exitoso.');
    refreshGrid();
}

const upsertModalOnCloseHandler = () => {
    refreshGrid();
    fillUpsertJugadorForm({});
}

const equipoModalOnCloseHandler = () => {
    refreshGrid();
    fillUpsertEquipoForm({});
}

const upsertFormSubmitHandler = async (e) => {
    e.preventDefault();
    const values = getObjectFromForm(e);
    const { Id, ...jugador } = values;

    const response = (Id === 0) ?
        await upsertData(upsertConfig, jugador) :
        await upsertData(upsertConfig, { Id, ...jugador }, 'PUT');

    const { hasError, errorMessage } = response

    if (hasError) {
        showToast(errorMessage, 'alert');
        return;
    }

    showToast('Jugador guardado exitosamente.');
    Metro.dialog.close(upsertModal)
}

const equipoFormSubmitHandler = async (e) => {
    e.preventDefault();
    const values = getObjectFromForm(e);
    const { ...jugador } = values;

    const response = await upsertData(upsertEquipoConfig, jugador, 'PUT');

    const { hasError, errorMessage } = response

    if (hasError) {
        showToast(errorMessage, 'alert');
        return;
    }

    showToast('Asignacion de equipo completada.');
    Metro.dialog.close(equipoModal)
}

//listeners
addButton.addEventListener('click', addButtonHandler);
upsertForm.addEventListener('submit', upsertFormSubmitHandler)
equipoForm.addEventListener('submit', equipoFormSubmitHandler)
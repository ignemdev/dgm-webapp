const getObjectFromForm = (e) => {
    const { target: { elements } } = e;
    const [...inputs] = elements;
    const values = inputs
        .filter((i) => (i.name != ''))
        .map(({ value, type, name }) => {
            if (type === 'hidden' || type === 'number' || (type = 'select-one' && !isNaN(value))) {
                return { value: Number(value), type, name };
            }
            return { value, type, name };
        })
        .reduce((acc, { name, value }) => (acc[name] = value, acc), {});

    return values;
}

const showToast = (message, type = 'success') => {
    Metro.toast.create(message, null, null, type);
}

const showConfirm = (confirmConfig, confirmFunction) => {
    Metro.dialog.create({
        clsDialog: 'alert mantenimiento-confirm-dialog',
        title: confirmConfig.title,
        content: `<div>${confirmConfig.message}</div>`,
        closeButton: true,
        actions: [
            {
                caption: 'Aceptar',
                cls: 'js-dialog-close alert',
                onclick: confirmFunction
            }
        ]
    });
}

const initSelect = async (config, defaultValue = null) => {
    const response = await fetch(config.url);
    const options = (await response.json()).reduce((acc, item) => (acc[item[config.value]] = item[config.label], acc), {})
    const select = $(config.selectId).data('select');
    select.data(options);

    if (!defaultValue) {
        return;
    };

    select.val(defaultValue);
}

const upsertData = async ({ url, ...rest }, object = {}, method = 'POST') => {
    const response = await fetch(url, { ...rest, body: JSON.stringify(object), method })

    if (response.status == 204) {
        return {}
    }

    return response.json();
}

const getData = async (url, method = 'GET') => {
    const response = await fetch(url, { method });
    const { result } = await response.json();
    return result;
}
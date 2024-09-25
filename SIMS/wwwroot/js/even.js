document.addEventListener('DOMContentLoaded', (event) => {
    const btn_add = document.querySelector('#btn-add');
    const btns_edit = document.querySelectorAll('#btn-edit');
    const btns_close = document.querySelectorAll('#btn-close');

    if (btn_add) {
        btn_add.addEventListener('click', () => {
            document.querySelector('.form-add-info').style.display = 'flex';
        });
    }

    //if (btns_edit) {
    //    btns_edit.forEach((btn_edit) => {
    //        btn_edit.addEventListener('click', (event) => {
    //            document.querySelector('.form-edit-info').style.display = 'flex';
    //        });
    //    });
    //}

    if (btns_close) {
        btns_close.forEach((btn_close) => {
            btn_close.addEventListener('click', () => {
                document.querySelector('.form-add-info').style.display = 'none';
                document.querySelector('.form-edit-info').style.display = 'none';
            })
        })
    }

    const params = new URLSearchParams(window.location.search);
    const showEditForm = params.get('showEditForm');

    if (showEditForm === 'true') {
        document.querySelector('.form-edit-info').style.display = 'flex';
    }
});
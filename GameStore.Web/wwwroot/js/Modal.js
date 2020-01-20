var modal = new tingle.modal({
    footer: true,
    stickyFooter: false,
    closeMethods: ['overlay', 'button', 'escape'],
    closeLabel: "Close",
    cssClass: ['custom-class-1', 'custom-class-2'],
    onOpen: function () {

    },
    onClose: function () {

    },
    beforeClose: function () {
        return true;
    }
});;

modal.init();

let btns = document.querySelectorAll(".trigger-button");

btns.forEach(function (elem) {
    elem.addEventListener('click', function () {
        let modalForm = document.querySelector('.tingle-modal-box__content');

        modalForm.childNodes.forEach(function (child) {
            modalForm.removeChild(child);
        });

        let deleteLink = document.createElement('a');
        let message = document.createElement('p');
        message.innerHTML = "Are you sure you want to delete this comment?";
        deleteLink.classList += "delete-link-modal";
        deleteLink.href = this.getAttribute("deleteUrl");
        deleteLink.innerHTML = "Delete";

        let divLink = document.createElement('div');

        divLink.appendChild(message);
        divLink.appendChild(deleteLink);

        modalForm.appendChild(divLink);

        modal.setContent(modalForm.innerHTML);
        modal.open();
    });
});

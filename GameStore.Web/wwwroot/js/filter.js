function Filter(urlAction) {
    let pageWrapper = document.getElementById("game-outline");
    let filterbtn = document.getElementById("filter-btn");
    let filterFormEvent = document.getElementById("filterForm");


    pageWrapper.addEventListener("click", function (e) {
        if (e.target.id == "filter-btn") {
            e.preventDefault();

            sendAsync();
        }

        if (e.target.id == "pagging-btn") {
            document.getElementById("currentPage").setAttribute("value", e.target.value);
            e.preventDefault();
            sendAsync();
        }
    });

    function sendAsync() {
        const finterForm = document.getElementById("filterForm");
        let formCreatingData = new FormData(finterForm);

        return fetch(urlAction,
            {
                method: 'POST',
                body: new URLSearchParams(formCreatingData)
            })
            .then(response => response.text())
            .then(text => {
                document.getElementById("game-outline").innerHTML = text;
                window.history.pushState("", "", insertParamsToURL(new URLSearchParams(formCreatingData)));
                window.scrollTo({ top: 655, behavior: 'smooth' });
            }).catch(err => {
                console.error('fetch failed', err);
            });
    }

    function insertParamsToURL(params) {
        return location.href.split('#')[0].split('?')[0] + '?' + params;
    }
}
let orderOutline = document.getElementById("basket-outline");

function RemoveItemBasket(urlAction) {
    orderOutline.addEventListener('click', function (e) {
        if (e.target.id == "remove-item-btn") {
            const gameKeyString = e.target.getAttribute("value");

            return fetch(urlAction,
                {
                    method: "POST",
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ gameKey: gameKeyString })
                })
                .then(response => response.text())
                .then(text => {
                    document.getElementById("basket-field").innerHTML = text;
                }).catch(err => {
                    console.error('fetch failed', err);
                });
        }
    });
}

function MinusItemBasket(urlAction) {
    orderOutline.addEventListener("click", function (e) {
        if (e.target.id == "bar-minus") {
            const orderDetailId = e.target.getAttribute("orderId");

            return fetch(urlAction,
                {
                    method: "POST",
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ Id: orderDetailId, Quantity: -1 })
                })
                .then(response => response.text())
                .then(text => {
                    document.getElementById("basket-field").innerHTML = text;
                })
                .catch(err => {
                    console.error('fetch failed', err);
                });
        }
    });
}

function PlusItemBasket(urlAction) {
    orderOutline.addEventListener("click", e => {
        if (e.target.id == "bar-plus") {
            const orderDetailId = e.target.getAttribute("orderId");

            return fetch(urlAction,
                {
                    method: "POST",
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ Id: orderDetailId, Quantity: 1 })

                })
                .then(response => response.text())
                .then(text => {
                    document.getElementById("basket-field").innerHTML = text;
                }).catch(err => {
                    console.error('fetch failed', err);
                });
        }
    });
}
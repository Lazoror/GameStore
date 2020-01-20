function LoadComments(urlAction) {
    let gameComments = document.getElementById("comments-wrapper");

    gameComments.addEventListener("click", function (e) {

        if (e.target.id == "comment-submit-btn") {
            e.preventDefault();

            const quoteForm = e.target.parentElement.parentElement;
            const formCreatingData = new FormData(quoteForm);

            return fetch(urlAction,
                {
                    method: 'POST',
                    body: new URLSearchParams(formCreatingData)

                })
                .then(response => response.text())
                .then(text => {
                    document.getElementById("comments-outline").innerHTML = text;
                }).catch(err => {
                    console.error('fetch failed', err);
                });
        }
    });
}

function AddComment(urlAction) {
    let commentCreatingForm = document.getElementById("add-comment-form");

    commentCreatingForm.addEventListener("submit", function (e) {
        e.preventDefault();

        const formCreatingData = new FormData(commentCreatingForm);

        return fetch(urlAction,
            {
                method: 'POST',
                body: new URLSearchParams(formCreatingData)
            })
            .then(response => response.text())
            .then(text => {
                let commentBody = document.getElementById("comment-area");
                commentBody.value = "";
                document.getElementById("comments").innerHTML = text;
            }).catch(err => {
                console.error('fetch failed', err);
            });
    });
}

function ChangeRating(urlAction) {
    let gameRatingOutline = document.getElementById("game-ratings");

    gameRatingOutline.addEventListener("click", function (e) {
        e.preventDefault();

        if (e.target.id == "game-star") {
            const starValue = e.target.getAttribute("value");
            const gameKey = e.target.getAttribute("gameKey");

            return fetch(urlAction,
                {
                    method: "POST",
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ Rating: starValue, Key: gameKey })
                })
                .then(response => response.text())
                .then(text => {
                    document.getElementById("game-stars-outline").innerHTML = text;
                }).catch(err => {
                    console.error('fetch failed', err);
                });
        }

    });
}

let commentQuote = document.querySelectorAll(".comment-quote-link");

commentQuote.forEach(function (element) {
    let clickCount = 0;

    element.addEventListener("click", function (e) {
        clickCount++;

        if (clickCount % 2 == 0) {
            e.target.parentElement.children[3].style.display = 'none';
        } else {
            e.target.parentElement.children[3].style.display = 'block';
        }
    });
});
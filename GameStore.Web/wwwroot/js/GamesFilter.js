function SetFilterGames(releaseDate, sortType, filters) {
    document.getElementById('release-date').value = releaseDate;
    document.getElementById('sort-type').value = sortType;

    let paggingForm = document.getElementById('pagination-items');
    let filterFormInputs = document.querySelectorAll("#filterForm input:not(.btn)");
    let filterFormSelects = document.querySelectorAll("#filterForm select");
    let filterBtn = document.getElementById('filter-btn');
    let filterForm = document.getElementById('filterForm');
    let isChanged = false;

    paggingForm.addEventListener("change", function () {
        this.submit();
    });

    filterBtn.addEventListener("click", function () {

        const stringFilter = str.split('&quot;').join('"');
        const oldFilters = JSON.parse(stringFilter);

        let jsonFilters = filters.split('&quot;').join('"');
        let currentFilters = JSON.parse(jsonFilters);

        let genres = document.querySelectorAll(".sidebar-genres input");
        let genresArr = [];
        for (let i = 0; i < genres.length; i++) {
            if (genres[i].checked == true) {
                genresArr.push(genres[i].value);
            }
        }

        if (genresArr.length == 0) {
            genresArr = null;
        }

        currentFilters.Genres = genresArr;

        let platforms = document.querySelectorAll(".sidebar-platforms input");
        let platformsArr = [];
        for (let i = 0; i < platforms.length; i++) {
            if (platforms[i].checked == true) {
                platformsArr.push(platforms[i].value);
            }
        }

        if (platformsArr.length == 0) {
            platformsArr = null;
        }

        currentFilters.Platforms = platformsArr;

        let publishers = document.querySelectorAll(".sidebar-publisher input");
        let publishersArr = [];
        for (let i = 0; i < publishers.length; i++) {
            if (publishers[i].checked == true) {
                publishersArr.push(publishers[i].value);
            }
        }

        if (publishersArr.length == 0) {
            publishersArr = null;
        }

        currentFilters.Publishers = publishersArr;
        currentFilters.ReleaseDate = Number(document.querySelector("#release-date").value);
        currentFilters.SortType = Number(document.querySelector("#sort-type").value);
        currentFilters.PriceFrom = Number(document.querySelector("#priceFrom").value);
        currentFilters.PriceTo = Number(document.querySelector("#priceTo").value);

        if (oldFilters.SortType != currentFilters.SortType) {
            isChanged = true;
        }
        if (oldFilters.PriceFrom != currentFilters.PriceFrom) {
            isChanged = true;
        }
        if (oldFilters.PriceTo != currentFilters.PriceTo) {
            isChanged = true;
        }
        if (oldFilters.Publishers != currentFilters.Publishers) {
            isChanged = true;
        }
        if (oldFilters.ReleaseDate != currentFilters.ReleaseDate) {
            isChanged = true;
        }
        if (oldFilters.Genres != currentFilters.Genres) {
            isChanged = true;
        }
        if (oldFilters.Platforms != currentFilters.Platforms) {
            isChanged = true;
        }

        if (isChanged == true) {
            document.getElementById("currentPage").setAttribute("value", 1);
            filterBtn.setAttribute("type", "submit");
            filterBtn.submit();
        }
    });
    isChanged = false;
    filterBtn.setAttribute("type", "button");
}
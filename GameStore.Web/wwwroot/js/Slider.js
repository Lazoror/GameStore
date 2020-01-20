
(function () {

    function Slideshow(element) {
        this.el = document.querySelector(element);
        this.init();
    }

    Slideshow.prototype = {
        init: function () {
            this.wrapper = this.el.querySelector(".slider-wrapper");
            this.slides = this.el.querySelectorAll(".slide");
            this.previous = this.el.querySelector(".slider-previous");
            this.next = this.el.querySelector(".slider-next");
            this.navigationLinks = this.el.querySelectorAll(".slider-pagination a");
            this.index = 0;
            this.total = this.slides.length;

            this.setup();
            this.actions();
        },
        _slideTo: function (slide) {
            let currentSlide = this.slides[slide];
            currentSlide.style.opacity = 1;

            for (let i = 0; i < this.slides.length; i++) {
                let slide = this.slides[i];
                if (slide !== currentSlide) {
                    slide.style.opacity = 0;
                }
            }
        },
        _highlightCurrentLink: function (link) {
            let self = this;
            for (let i = 0; i < self.navigationLinks.length; ++i) {
                let a = self.navigationLinks[i];
                a.className = "";
            }
            link.className = "current";
        },
        setup: function () {
            let self = this;

            for (let l = 0; l < self.slides.length; ++l) {
                let elSlide = self.slides[l];
                let image = elSlide.getAttribute("data-image");
                elSlide.style.backgroundImage = "url(" + image + ")";
            }

            for (let k = 0; k < self.navigationLinks.length; ++k) {
                let pagLink = self.navigationLinks[k];
                pagLink.setAttribute("data-index", k);
            }
        },
        actions: function () {

            let self = this;

            self.next.addEventListener("click", function () {
                self.index++;
                self.previous.style.display = "block";

                if (self.index == self.total - 1) {
                    self.index = self.total - 1;
                    self.next.style.display = "none";
                }

                self._slideTo(self.index);

                self._highlightCurrentLink(self.navigationLinks[self.index]);




            }, false);

            self.previous.addEventListener("click", function () {
                self.index--;
                self.next.style.display = "block";

                if (self.index == 0) {
                    self.index = 0;
                    self.previous.style.display = "none";
                }

                self._slideTo(self.index);

                self._highlightCurrentLink(self.navigationLinks[self.index]);



            }, false);

            for (let i = 0; i < self.navigationLinks.length; ++i) {
                let a = self.navigationLinks[i];

                a.addEventListener("click", function (e) {
                    e.preventDefault();
                    let n = parseInt(this.getAttribute("data-index"), 10);

                    self.index = n;

                    if (self.index == 0) {
                        self.index = 0;
                        self.previous.style.display = "none";
                    }

                    if (self.index > 0) {
                        self.previous.style.display = "block";
                    }

                    if (self.index == self.total - 1) {
                        self.index = self.total - 1;
                        self.next.style.display = "none";
                    } else {
                        self.next.style.display = "block";
                    }

                    self._slideTo(self.index);

                    self._highlightCurrentLink(this);



                }, false);
            }
        }


    };
})();

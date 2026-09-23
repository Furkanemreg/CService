//(function () {
//    function debounce(fn, delay) {
//        let timer = null;
//        return function (...args) {
//            clearTimeout(timer);
//            timer = setTimeout(() => fn.apply(this, args), delay);
//        };
//    }

//    function initSearchSelect(container) {
//        const url = container.getAttribute('data-search-url');
//        const hiddenInput = container.querySelector('.search-select-hidden');
//        const textInput = container.querySelector('.search-select-input');
//        const resultsBox = container.querySelector('.search-select-results');

//        function showResults(items) {
//            resultsBox.innerHTML = '';
//            if (!items.length) {
//                resultsBox.classList.remove('open');
//                return;
//            }

//            items.forEach(function (item) {
//                const row = document.createElement('div');
//                row.className = 'search-select-item';
//                row.textContent = item.text;
//                row.addEventListener('click', function () {
//                    hiddenInput.value = item.id;
//                    textInput.value = item.text;
//                    resultsBox.classList.remove('open');
//                });
//                resultsBox.appendChild(row);
//            });

//            resultsBox.classList.add('open');
//        }

//        const search = debounce(async function () {
//            const q = textInput.value.trim();
//            hiddenInput.value = '';

//            if (q.length < 1) {
//                resultsBox.classList.remove('open');
//                return;
//            }

//            const response = await fetch(url + '?q=' + encodeURIComponent(q));
//            if (!response.ok) return;
//            const items = await response.json();
//            showResults(items);
//        }, 250);

//        textInput.addEventListener('input', search);

//        document.addEventListener('click', function (e) {
//            if (!container.contains(e.target)) {
//                resultsBox.classList.remove('open');
//            }
//        });
//    }

//    document.querySelectorAll('[data-search-select]').forEach(initSearchSelect);
//})();
(function () {
    function debounce(fn, delay) {
        let timer = null;
        return function (...args) {
            clearTimeout(timer);
            timer = setTimeout(() => fn.apply(this, args), delay);
        };
    }

    function initSearchSelect(container) {
        const url = container.getAttribute('data-search-url');
        const hiddenInput = container.querySelector('.search-select-hidden');
        const textInput = container.querySelector('.search-select-input');
        const resultsBox = container.querySelector('.search-select-results');
        const iconBox = container.querySelector('.search-select-icon');

        function showResults(items) {
            resultsBox.innerHTML = '';
            if (!items.length) {
                resultsBox.classList.remove('open');
                return;
            }

            items.forEach(function (item) {
                const row = document.createElement('div');
                row.className = 'search-select-item';
                row.textContent = item.text;
                row.addEventListener('click', function () {
                    hiddenInput.value = item.id;
                    textInput.value = item.text;
                    resultsBox.classList.remove('open');
                });
                resultsBox.appendChild(row);
            });

            resultsBox.classList.add('open');
        }

        async function performSearch(q) {
            hiddenInput.value = '';
            const response = await fetch(url + '?q=' + encodeURIComponent(q));
            if (!response.ok) return;
            const items = await response.json();
            showResults(items);
        }

        const debouncedSearch = debounce(function () {
            performSearch(textInput.value.trim());
        }, 250);

        textInput.addEventListener('input', debouncedSearch);

        iconBox?.addEventListener('click', function () {
            performSearch(textInput.value.trim());
            textInput.focus();
        });

        document.addEventListener('click', function (e) {
            if (!container.contains(e.target)) {
                resultsBox.classList.remove('open');
            }
        });
    }

    document.querySelectorAll('[data-search-select]').forEach(initSearchSelect);
})();
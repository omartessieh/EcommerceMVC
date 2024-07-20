/*Carousel*/
$(document).ready(function () {
    $("#blogCarousel").carousel({ interval: 2000 });
});

/*Scroll Up*/
window.onscroll = function () { scrollFunction() };

function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        document.getElementById("scrollToTopBtn").style.display = "block";
    } else {
        document.getElementById("scrollToTopBtn").style.display = "none";
    }
}

document.getElementById("scrollToTopBtn").addEventListener("click", function () {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
});

//Search Function
$(document).ready(function () {
    $('#searchInput').on('input', function () {
        var searchString = $(this).val();
        var $searchResults = $('#searchResults');
        if (searchString.length >= 1) {
            $.ajax({
                url: '/Products/Search',
                type: 'GET',
                data: { searchString: searchString },
                success: function (data) {
                    $searchResults.empty();
                    if (data.length > 0) {
                        $.each(data, function (index, item) {
                            $searchResults.append('<li class="option" onclick="selectSearchResult(' + item.id + ', \'' + item.title + '\', \'' + item.type + '\')"><span class="option-text" style="color: black;">' + item.title + '</span></li>');
                        });
                        $searchResults.show();
                    } else {
                        $searchResults.hide();
                    }
                }
            });
        } else {
            $searchResults.hide();
        }
    });
});

function selectSearchResult(id, title, type) {
    if (type == "Category") {
        window.location.href = '/Products/Index/' + id + '?title=' + title;
    } else {
        window.location.href = '/Products/ProductDetails/' + id;
    }
}

//Sort Products Price
function sortProducts() {
    var selectElement = document.getElementById("sortSelect");
    var selectedValue = selectElement.options[selectElement.selectedIndex].value;
    var products = document.querySelectorAll('.product-grid');

    var sortedProducts = Array.from(products).sort(function (a, b) {
        var priceA = parseFloat(a.querySelector('.price span').innerText.replace('$', ''));
        var priceB = parseFloat(b.querySelector('.price span').innerText.replace('$', ''));

        if (selectedValue === 'Low') {
            return priceA - priceB;
        } else if (selectedValue === 'High') {
            return priceB - priceA;
        } else {
            return 0;
        }
    });

    var productContainer = document.getElementById('productContainer');
    productContainer.innerHTML = '';

    sortedProducts.forEach(function (product) {
        productContainer.appendChild(product.parentNode.cloneNode(true));
    });
}
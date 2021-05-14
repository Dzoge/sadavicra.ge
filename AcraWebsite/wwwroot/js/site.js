function filterVaccines(vaccineId) {
    filterElems('vaccine', vaccineId);
}
function filterRegions(regionId) {
    filterElems('region', regionId);
}
function filterDate(date) {
    var filterValue = "";
    if (!!date) {
        var dateValue = new Date(date);
        var day = ('0' + dateValue.getDate()).substr(-2);
        var month = ('0' + (dateValue.getMonth() + 1)).substr(-2);
        var year = dateValue.getFullYear();
        var filterValue = year + '-' + month + '-' + day;
    }
    filterElems('date', filterValue);
}
function filterElems(dataSetName, value) {
    $('[data-' + dataSetName + ']').each(function () {
        var elem = this;
        var sholdShow = !value
            ? true
            : elem.dataset[dataSetName] == value;
        elem.classList.toggle('hide', !sholdShow);
    });

    var url = new URL(window.location.href);
    url.searchParams.delete(dataSetName);
    if (value)
        url.searchParams.set(dataSetName, value);
    history.replaceState({}, document.title, url.toString());
}
function appendQueryParameter(url, name, value) {
    if (url.length === 0) {
        return;
    }

    var rawURL = url;

    if (rawURL.charAt(rawURL.length - 1) === "?") {
        rawURL = rawURL.slice(0, rawURL.length - 1);
    }

    var parsedURL = new URL(rawURL);
    var parameters = parsedURL.search;

    parameters += (parameters.length === 0) ? "?" : "&";
    parameters = (parameters + name + "=" + value);

    return (parsedURL.origin + parsedURL.pathname + parameters);
}

$('#selectRegion').on('change', function () {
    filterRegions(this.value);
});
$('#selectVaccine').on('change', function () {
    filterVaccines(this.value);
});
$('#selectDate').on('change', function () {
    filterDate(this.value);
});


$(document).on("click", ".js-slots-toggle", function () {
    var $toggle = $(this);
    var $locationWrap = $toggle.parents('.js-location-wrap');
    var $slotsContainer = $locationWrap.find('.js-location-slots');
    var $slotsContainerContent = $locationWrap.find('.js-location-slots-content');

    if ($locationWrap.hasClass('open')) {
        $slotsContainer.slideUp(400, function () {
            $slotsContainerContent.html('');
            $locationWrap.removeClass('open');
        });
    } else {
        $locationWrap.addClass('loading');

        var request = {
            "regionId": $toggle.data("region-id"),
            "serviceId": $toggle.data("service-id"),
            "branchId": $toggle.data("branch-id")
        };
        $.get("/home/getslots", request)
            .done(function (response) {
                $slotsContainerContent.html(response);
                $('#selectDate').trigger('change');
                $slotsContainer.slideDown();
                $locationWrap.addClass('open');
            })
            .always(function () {
                $locationWrap.removeClass('loading');
            });
    }
})
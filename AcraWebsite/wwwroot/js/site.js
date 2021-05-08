function filterVaccines(vaccineId) {
    filterElems('vaccine', vaccineId);
}
function filterRegions(regionId) {
    filterElems('region', regionId);
}
function filterElems(dataSetName, value) {
    $('[data-' + dataSetName + ']').each(function () {
        var elem = this;
        var sholdShow = !value
            ? true
            : elem.dataset[dataSetName] == value;
        elem.classList.toggle('hide', !sholdShow);
    });
}

$('#selectRegion').on('change', function () {
    filterRegions(this.value);
});
$('#selectVaccine').on('change', function () {
    filterVaccines(this.value);
});
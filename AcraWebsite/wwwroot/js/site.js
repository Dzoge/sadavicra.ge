function filterVaccines(vaccineId) {
    filterElems('vaccine', vaccineId);
}
function filterRegions(regionId) {
    filterElems('region', regionId);
}
function filterElems(dataSetName, value) {
    [].forEach.call(querySelectorAll('[data-' + dataSetName + ']'), (elem) => {
        var sholdShow = !value
            ? true
            : elem.dataset[dataSetName] = value;
        elem.classList.toggle('hide', sholdShow);
    });
}
var Home = function () {
    var generateGrid = function (model1) {
        NB.generateGridList("#personGrid", model1,
        [
            {
                title: "Kişi",
                value: "name"
            },
            {
                title: "TC No",
                value: "tckn"
            },
            {
                title: "Telefon Numarası",
                value: "phone"
            },
            {
                title: "Doğum Yeri",
                value: "birthPlace"
            }
                ]);
    }
    return {
        init: function (model) {
            generateGrid(model);
        }
    }
}();
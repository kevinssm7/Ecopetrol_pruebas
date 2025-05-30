var detalleViewModel = function () {
    var self = this;

    self.correos = ko.observable();

    ko.validatedObservable(self);
};

var correoViewModel = function () {
    var self = this;

    self.detalle = ko.observableArray();

    self.agregarDetalle = function () {
        self.detalle.push(new detalleViewModel());
    };

    self.eliminarDetalle = function (detalle) {
        self.detalle.remove(detalle);
    };

    self.canCreate = function () {
        var detallesValidos = self.detalle().length > 0;

        ko.utils.arrayForEach(self.detalle(), function (item) {
            detallesValidos = detallesValidos && item.isValid();
        });

        return detallesValidos;
    };
};
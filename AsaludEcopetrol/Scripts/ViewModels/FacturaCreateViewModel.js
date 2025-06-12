var detalleViewModel = function () {
    var self = this;

       
    self.numeroFactura = ko.observable(0);
    self.valorFactura = ko.observable(0);
    self.valorGlosa = ko.observable(0);
    self.id_motivo = ko.observable(0);
    self.descripcion_devolucion = ko.observable();
    self.observaciones = ko.observable();

    ko.validatedObservable(self);
};

var facturaViewModel = function () {
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
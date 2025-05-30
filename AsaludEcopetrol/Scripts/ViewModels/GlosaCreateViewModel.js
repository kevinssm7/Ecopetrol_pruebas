var detalleViewModel = function () {
    var self = this;

    self.motivo_glosa = ko.observable(0);
    self.valor_glosa = ko.observable(0);
    self.observaciones = ko.observable();
    
    // se aplican las validaciones definidas en el html
    ko.validatedObservable(self);
};

var GlosaViewModel = function () {
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
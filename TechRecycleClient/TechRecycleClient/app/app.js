(function () {
    var app = angular.module('techRecycleApp', ['ngRoute', 'ngAnimate', 'ui.bootstrap']);

    app.config(['$routeProvider', function ($routeProvider) {
        $routeProvider.
        when('/Dashboard', {
            templateUrl: '/app/views/dashboard.html',
            controller: 'DashboardCtrl'
        })
        .when('/TicketCreate', {
            templateUrl: '/app/views/ticketCreate.html',
            controller: 'TicketCreateCtrl'
        })
        .otherwise({
            redirectTo: '/Dashboard'
        });
    }]);

    app.service('modalService', ['$modal',
    function ($modal) {

        var modalDefaults = {
            backdrop: true,
            keyboard: true,
            modalFade: true,
            templateUrl: '/app/partials/modal.html'
        };

        var modalOptions = {
            closeButtonText: 'Close',
            actionButtonText: 'Ok',
            headerText: 'Proceed?',
            bodyText: 'Perform this action?',
            showHeader: false
        };

        this.showModal = function (customModalDefaults, customModalOptions) {
            if (!customModalDefaults) customModalDefaults = {};
            //customModalDefaults.backdrop = 'static';
            return this.show(customModalDefaults, customModalOptions);
        };

        this.show = function (customModalDefaults, customModalOptions) {
            //Create temp objects to work with since we're in a singleton service
            var tempModalDefaults = {};
            var tempModalOptions = {};

            //Map angular-ui modal custom defaults to modal defaults defined in service
            angular.extend(tempModalDefaults, modalDefaults, customModalDefaults);

            //Map modal.html $scope custom properties to defaults defined in service
            angular.extend(tempModalOptions, modalOptions, customModalOptions);

            if (!tempModalDefaults.controller) {
                tempModalDefaults.controller = function ($scope, $modalInstance) {
                    $scope.modalOptions = tempModalOptions;
                    $scope.modalOptions.ok = function (result) {
                        $modalInstance.close(result);
                    };
                    $scope.modalOptions.close = function (result) {
                        $modalInstance.dismiss('cancel');
                    };
                }
            }

            return $modal.open(tempModalDefaults).result;
        };

    }]);

    app.controller('MainCtrl', ['$scope', function ($scope) {
        //$scope.modalShown = false;
        //$scope.toggleModal = function () {
        //    $scope.modalShown = !$scope.modalShown;
        //};
    }]);

    app.controller('NavCtrl', ['$scope', '$location', function ($scope, $location) {
        $scope.isActive = function (path) {
            return ($location.path() === path);
        };
    }]);

    app.controller('DashboardCtrl', ['$scope', '$http', function ($scope, $http) {
        // no items needed here as of now - scope is 
    }]);

    app.controller('TicketCreateCtrl', ['$scope', '$location', '$http', 'modalService', function ($scope, $location, $http, modalService) {
        function BoxValidationError(box, message) {
            this.message = message;
            this.box = 0;
        }
        BoxValidationError.prototype = Object.create(Error.prototype);
        BoxValidationError.prototype.name = "BoxValidationError";

        $scope.selectedBox = 0;
        $scope.boxes = [
            { isOpen: true },
            { isOpen: false },
            { isOpen: false },
            { isOpen: false },
            { isOpen: false }
        ];

        $scope.ticket = {
            alias: "",
            name: "",
            phone: "",
            email: "",
            alternateName: "",
            alternatePhone: "",
            alternateEmail: "",
            building: "",
            location: "",
            computerQuantity: "",
            monitorQuantity: "",
            miscQuantity: "",
            pickupSize: "",
            binQuantity: "",
            bins: [
                { number: 1, name: "bin1", location: "" },
                { number: 2, name: "bin2", location: "" },
                { number: 3, name: "bin3", location: "" },
                { number: 4, name: "bin4", location: "" },
                { number: 5, name: "bin5", location: "" }],
            secureBinQuantity: "",
            secureBins: [
                { number: 1, name: "secureBin1", location: "" },
                { number: 2, name: "secureBin2", location: "" },
                { number: 3, name: "secureBin3", location: "" },
                { number: 4, name: "secureBin4", location: "" },
                { number: 5, name: "secureBin5", location: "" }],
            destructionLocation: "",
            witnessType: "",
            additionalNotes: "",
        };

        $scope.buildings = ['1', '2', '3', '4', '5', '6', '8', '9', '10', '11', '16', '17', '18', '19', '21', '22', '24', '25', '26', '27', '28',
        '30', '31', '32', '33', '34', '35', '36', '37', '40', '41', '42', '43', '44', '50', '84', '85', '86', '87', '88', '92', '99', '109',
        '110', '111', '112', '113', '114', '115', '120', '121', '122', '123', '124', '125', '126', '127', 'Advanta A', 'Advanta C',
        'Bravern 1', 'Bravern 2', 'Commons', 'Kirkland 434', 'Kirkland 550', 'Redwest A', 'Redwest B', 'Redwest C', 'Redwest D', 'Redwest E',
        'Redwest F', 'Redwoods A', 'Redwoods B', 'Redwoods C', 'Studio A', 'Studio B', 'Studio C', 'Studio D', 'Studio E',
        'Studio F', 'Studio G', 'Studio H'];

        $scope.showErrorDialogBox = function (textBody) {
            if (textBody == null || textBody == '') textBody = "There was an error";
            var modalOptions = {
                bodyText: textBody
            };

            modalService.showModal({}, modalOptions);
        }

        $scope.isActive = function (box) {
            return $scope.selectedBox === box;
        };

        $scope.selectBox = function (box) {
            try {
                $scope.validateBoxesBefore(box);
                $scope.resetBoxes();
                $scope.boxes[box].isOpen = true;
                $scope.selectedBox = box;
            }
            catch (e) {
                $scope.boxes[$scope.selectedBox].isOpen = true;
                $scope.showErrorDialogBox(e.message);
            }
        };

        $scope.validateBoxesBefore = function (box) {
            for (i = box - 1; i >= 0; i--) {
                switch (i) {
                    case 0:
                        if ($scope.formScope.form.name.$error.required || $scope.formScope.form.phone.$error.required || $scope.formScope.form.email.$error.required
                            || $scope.formScope.form.alternateName.$error.required || $scope.formScope.form.alternatePhone.$error.required || $scope.formScope.form.alternateEmail.$error.required)
                            throw new BoxValidationError(0, "Please complete the required fields.");
                        if ($scope.formScope.form.email.$error.email || $scope.formScope.form.alternateEmail.$error.email)
                            throw new BoxValidationError(0, "Please enter a valid email address.");
                        break;
                    case 1:
                        if ($scope.formScope.form.building.$error.required || $scope.formScope.form.location.$error.required)
                            throw new BoxValidationError(1, "Please complete the required fields.");
                        break;
                    case 2:
                        if ($scope.ticket.pickupSize == "" || $scope.ticket.isHBIRequest == null) {
                            throw new BoxValidationError(2, "Please complete form fields.");
                        }
                        else if (!($scope.ticket.computerQuantity > 0 || $scope.ticket.monitorQuantity > 0 || $scope.ticket.miscQuantity > 0)){
                            throw new BoxValidationError(2, "Please fill in at least one quantity field. The fields only accept numbers.");
                        }
                        else if ($scope.ticket.pickupSize == "Bulk") {
                            if ($scope.formScope.form.binQuantity.$error.required) {
                                throw new BoxValidationError(2);
                            }
                            for (j = 1; j <= $scope.formScope.form.binQuantity.$viewValue; j++) {
                                switch (j) {
                                    case 1:
                                        if ($scope.formScope.form.bin1.$error.required)
                                            throw new BoxValidationError(2, "Please complete required form fields.");
                                        break;
                                    case 2:
                                        if ($scope.formScope.form.bin2.$error.required)
                                            throw new BoxValidationError(2, "Please complete required form fields.");
                                        break;
                                    case 3:
                                        if ($scope.formScope.form.bin3.$error.required)
                                            throw new BoxValidationError(2, "Please complete required form fields.");
                                        break;
                                    case 4:
                                        if ($scope.formScope.form.bin4.$error.required)
                                            throw new BoxValidationError(2, "Please complete required form fields.");
                                        break;
                                    case 5:
                                        if ($scope.formScope.form.bin5.$error.required)
                                            throw new BoxValidationError(2, "Please complete required form fields.");
                                        break;
                                };
                            };
                        };
                        break;
                    default:
                        break;
                }
            }
        }

        $scope.resetBoxes = function () {
            $scope.boxes.forEach(function (b) {
                b.isOpen = false;
            });
        };

        $scope.setFormScope = function (scope) {
            $scope.formScope = scope;
        }

        $scope.ticketCreate = function () {
            for (i = 0; i < $scope.ticket.binQuantity; i++) {
                switch (i) {
                    case 0:
                        $scope.ticket.binLocation1 = $scope.formScope.form.bin1.$viewValue;
                        break;
                    case 1:
                        $scope.ticket.binLocation2 = $scope.formScope.form.bin2.$viewValue;
                        break;
                    case 2:
                        $scope.ticket.binLocation3 = $scope.formScope.form.bin3.$viewValue;
                        break;
                    case 3:
                        $scope.ticket.binLocation4 = $scope.formScope.form.bin4.$viewValue;
                        break;
                    case 4:
                        $scope.ticket.binLocation5 = $scope.formScope.form.bin5.$viewValue;
                        break;
                    default:
                        break;
                }
            }
            $http({
                method: "post",
                url: "/Tickets/Create",
                data: $scope.ticket
            }).success(function (data) {
                var modalOptions = {
                    bodyText: "Ticket number " + data.TicketNumber + " has been created."
                };

                modalService.showModal({}, modalOptions).then(function () {
                    $location.path('/Dashboard');
                    location.reload(true);
                });

            }).error(function (data) {
                var modalOptions = {
                    bodyText: "Error creating ticket - please contact support."
                };

                modalService.showModal({}, modalOptions).then(function () {
                    $location.path('/Dashboard');
                    location.reload(true);
                });
            });
        }
    }]);

    app.directive('highlightOnHover', function () {
        return {
            restrict: 'A',
            link: function ($scope, element, attrs) {
                if (/accordion/.test(element[0].outerHTML)) {
                    element.bind('mouseenter', function () {
                        element.removeClass('inactive-box');
                        element.addClass('hover-box');
                    });
                    element.bind('mouseleave', function () {
                        element.removeClass('hover-box');
                        element.addClass('inactive-box');
                    });
                } else if (/tile/.test(element[0].outerHTML)) {
                    element.bind('mouseenter', function () {
                        element.addClass('hover-box');
                    });
                    element.bind('mouseleave', function () {
                        element.removeClass('hover-box');
                    });

                } else {
                    element.bind('mouseenter', function () {
                        element.addClass('hover-link');
                    });
                    element.bind('mouseleave', function () {
                        element.removeClass('hover-link');
                    });
                }
            }
        }
    });

    app.directive('hdrAdaptWidth', function () {
        return {
            restrict: 'A',
            scope: {},
            link: function ($scope, element, attrs) {
                window.onload = function () {
                    var width = document.getElementsByTagName('header')[0].offsetWidth - 160;
                    element.css('width', width + 'px');
                }
                window.onresize = function () {
                    var width = document.getElementsByTagName('header')[0].offsetWidth - 160;
                    element.css('width', width + 'px');
                }
            }
        }
    });

    app.directive('fixedHeader', ['$timeout', function ($timeout) {
        return {
            restrict: 'A',
            scope: {
                tableHeight: '@'
            },
            link: function ($scope, $elem, $attrs, $ctrl) {
                // wait for content to load into table and the tbody to be visible
                $scope.$watch(function () { return $elem.find("tbody").is(':visible') },
                    function (newValue, oldValue) {
                        if (newValue === true) {
                            // reset display styles so column widths are correct when measured below
                            $elem.find('thead, tbody, tfoot').css('display', '');

                            // wrap in $timeout to give table a chance to finish rendering
                            $timeout(function () {
                                // set widths of columns
                                $elem.find('th').each(function (i, thElem) {
                                    thElem = $(thElem);
                                    var tdElems = $elem.find('tbody tr:first td:nth-child(' + (i + 1) + ')');
                                    var tfElems = $elem.find('tfoot tr:first td:nth-child(' + (i + 1) + ')');

                                    var columnWidth = tdElems.width();
                                    thElem.width(columnWidth);
                                    tdElems.width(columnWidth);
                                    tfElems.width(columnWidth);
                                });

                                // set css styles on thead and tbody
                                $elem.find('thead, tfoot').css({
                                    'display': 'block',
                                });

                                $elem.find('tbody').css({
                                    'display': 'block',
                                    'height': $scope.tableHeight || '400px',
                                    'overflow': 'auto'
                                });

                                // reduce width of last column by width of scrollbar
                                var scrollBarWidth = $elem.find('thead').width() - $elem.find('tbody')[0].clientWidth;
                                if (scrollBarWidth > 0) {
                                    // for some reason trimming the width by 2px lines everything up better
                                    scrollBarWidth -= 2;
                                    $elem.find('tbody tr:first td:last-child').each(function (i, elem) {
                                        $(elem).width($(elem).width() - scrollBarWidth);
                                    });
                                }
                            });
                        }
                    });
            }
        }
    }]);

    app.directive('onlyDigits', function () {

        return {
            restrict: 'A',
            require: '?ngModel',
            link: function (scope, element, attrs, ngModel) {
                if (!ngModel) return;
                ngModel.$parsers.unshift(function (inputValue) {
                    var digits = inputValue.split('').filter(function (s) { return (!isNaN(s) && s != ' '); }).join('');
                    ngModel.$viewValue = digits;
                    ngModel.$render();
                    return digits;
                });
            }
        }
    });

})();
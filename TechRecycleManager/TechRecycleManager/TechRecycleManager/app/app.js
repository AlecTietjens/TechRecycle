(function () {
    var app = angular.module('techRecycleApp', ['ngRoute', 'ngAnimate', 'ui.bootstrap']);

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

    app.controller('ManagerCtrl', ['$scope', '$http', function ($scope, $http) {
        //$scope.getMyCtrlScope = function () {
        //    return $scope;
        //}
        
        $scope.selectedTicket = {};

        $scope.tickets = {};
        $http.get('/Manager/GetAll').
        success(function (data) {
            $scope.tickets = JSON.parse(data);
        }).
        error(function (data) {

        });

        $scope.selectTicket = function(ticket) {
            $scope.selectedTicket = ticket;
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
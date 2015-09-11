deps.push('angularBootstrapNavTree');

if (angular.version.full.indexOf("1.2") >= 0) {
    deps.push('ngAnimate');
}

(function () {
    app.controller('designationController', function ($scope, $timeout, $http, Erpmodal, $validation) {
        var state;
        $scope.DesignationIdNName = new Object();
        var current, rawdata;
        var parents = [];
        $scope.DesignationId = 0;
        var init = function () {
            state = 0;
            current = null;
            rawdata = null;

            $scope.ShowLoading = false;

            $scope.ButtonDeleteDisabled = true;
            //for checkboxes

            $scope.selected = false;//for current selection

            $scope.HrmDesignation = {};
            $scope.HrmDesignation.information = {};
            $scope.HrmDesignation.parents;
            $scope.HrmDesignation.childrens;

            $scope.my_data = [];

            parents = [];
            $scope.DesignationId = 0;
            $scope.ShowLoading = true;
            $http.get("/Hrm/Designation/GetByCompanyId").success(function (data) {
                $scope.ShowLoading = false;
                rawdata = _.sortBy(data, function (item) {
                    return item.HrmDesignationId;
                });
                $scope.DesignationIdNName = data;
                $scope.my_data = createTreeMap(null, data);

            });
        }//end of init

        init();//init is called

        function createTreeMap(parentId, list) {
            var nodes = [];
            for (var i = 0, l; l = list[i]; i++) {
                if (l.HrmDesignationId === parentId) {
                    // list.splice(i, 1); i--;
                    nodes.push({
                        Id: l.Id
                      , HrmDesignationId: l.HrmDesignationId
                      , Name: l.Name
                      , ShortName: l.ShortName
                      , CmnCompanyId: l.CmnCompanyId
                      , children: createTreeMap(l.Id, list)
                    });
                }
            }
            return nodes;
        }//end of findChildren

        function findParent(parentId, list) {
            for (var i = 0, l; l = list[i]; i++) {
                if (l.Id === parentId) {
                    parents.push({
                        Id: l.Id
                     , HrmDesignationId: l.HrmDesignationId
                     , Name: l.Name
                     , ShortName: l.ShortName
                    }
                    );

                    findParent(l.HrmDesignationId, list);
                }
            }
        }//end of findParent

        $scope.my_tree_handler = function (branch) {
            $scope.selected = true;
            $scope.ButtonDeleteDisabled = false;
            current = branch;
            state = 1;
            $scope.DesignationId = branch.HrmDesignationId;
            $scope.HrmDesignation.information = {
                Id: branch.Id
                          , HrmDesignationId: branch.HrmDesignationId
                          , Name: branch.Name
                          , ShortName: branch.ShortName
                          , CmnCompanyId: branch.CmnCompanyId
            };

            $scope.ButtonNewDisabled = $scope.HrmDesignation.information.DepthLevel == 6 || $scope.HrmDesignation.information.IsTransactionalHead == true ? true : false;


            $scope.HrmDesignation.childrens = _.where(branch.children, { HrmDesignationId: branch.Id });


            parents = [];
            findParent(branch.HrmDesignationId, rawdata);
            $scope.HrmDesignation.parents = _.sortBy(parents, function (item) {
                return item.Id;
            });
        };//end of tree_handler

        $scope.Save = function () {
            
            Erpmodal.Confirm('Save').then(function (result) {
                 ;                
                $scope.HrmDesignation.information.HrmDesignationId = $scope.DesignationId;
                $http.post("/Hrm/Designation/Save", $scope.HrmDesignation.information).success(

                    function (data) {
                        Erpmodal.Save(data, "Save");
                      // current.push($scope.HrmDesignation.information);
                        $scope.Reset();
                        $validation.reset($scope.Account);
                    }

                    ).error(function (error) {
                    });
            });
            
        };//end of Save

       

        $scope.New = function () {
           
                state = 1;// now in New Mode

                $scope.ButtonNewDisabled = true;
                $scope.ButtonDeleteDisabled = true;

                //$scope.ScheduleNoDisabled = true;

                // $scope.TransactionalHeadDisabled = true;

                $scope.ScheduleNoDisabled = current.DepthLevel + 1 == 3 ? false : true;

                $scope.TransactionalHeadDisabled = current.DepthLevel + 1 > 3 ? false : true;

                $scope.HrmDesignation.information = {};//reset the form

            
        }//end of New

        $scope.Reset = function () {
            init();
            $validation.reset($scope.Account);
        }//end of Reset

        $scope.Delete = function () {
            Erpmodal.Confirm('Delete').then(function (result) {

                var Id = current.Id;
                $http.post("/Hrm/Designation/Delete", { Id: Id }).success(function (data) {
                    Erpmodal.Delete(data,"Delete");
                    init();
                    $validation.reset($scope.Account);
                }).error(function () {

                });// end of http
            });// end of then
        };// end of delete
    });
}).call(this);
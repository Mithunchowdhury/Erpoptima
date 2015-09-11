
angular.module('RecursionHelper', []).factory('RecursionHelper', ['$compile', function ($compile) {
    return {
        compile: function (element, link) {
            
            if (angular.isFunction(link)) {
                link = { post: link };
            }

            
            var contents = element.contents().remove();
            var compiledContents;
            return {
                pre: (link && link.pre) ? link.pre : null,
               
                post: function (scope, element) {
                   
                    if (!compiledContents) {
                        compiledContents = $compile(contents);
                    }
                   
                    compiledContents(scope, function (clone) {
                        element.append(clone);
                    });
                    if (link && link.post) {
                        link.post.apply(null, arguments);
                    }
                }
            };
        }
    };
}]);
deps.push('RecursionHelper');

app.directive("menu", function(RecursionHelper) {
    return {
        restrict: "A",
        scope: { family: '=' },
        terminal:true,
        //template: 
        //    '<ul>' + 
        //        '<li ng-repeat="child in family.children">' + 
        //            '<tree family="child"></tree>' +
        //        '</li>' +
        //    '</ul>',
        template:
            '<li ng-repeat="node in family" ng-class="{treeview:node.items.length>0}">' +
               '<a ng-href="{{node.Name}}">' +
                 '<span>{{node.DisplayName}}</span>'+
                  '<i ng-if="node.items" class="fa fa-angle-left pull-right"></i>'+
                '</a>'+
                '<ul ng-if="node.items" class="treeview-menu" menu family="node.items">' +
                '</ul>'+
             '<li>' ,
        compile: function(element) {
            return RecursionHelper.compile(element, function(scope, iElement, iAttrs, controller, transcludeFn){
                // Define your normal link function here.
                // Alternative: instead of passing a function,
                // you can also pass an object with 
                // a 'pre'- and 'post'-link function.
            });
        }
    };
});

app.controller('menuController', function ($scope, $http, $timeout) {

    $scope.Menus = [];
    var normalData = [];
    var menuData = [];
  

    var init = function () {

        $http.get("/Home/GetMenuByUserIdAndModuleId").success(function (data) {
            
            normalData = data;
            menuData = createTreeMap(null, normalData);   

            if (data[0].SecModuleId == 2) {
                $http.get("/Home/GetProcessLevelMenu").success(function (mdata) {
                    if (data[0].SecModuleId != 1) {//1 for security module
                        menuData.push({
                            Id: 0
                                  , SecResourceId: null
                                  , Name: '#'
                                  , DisplayName: 'Approval'
                                  , items: []
                        });

                        createTreeMapForApproval(null, mdata);
                    }

                    $scope.Menus = menuData;
                    $timeout(function () {
                        $(".sidebar .treeview").tree();
                    }, 3);

                }).error(function (error) {

                });
            }
            else {
                $scope.Menus = createTreeMap(null, normalData);
                $timeout(function () {
                    $(".sidebar .treeview").tree();
                }, 3);
                menuData = createTreeMap(null, normalData);
            }
            
                


        }).error(function (error) {



        });
        


    }// end of init

    init();
    
    function createTreeMap(parentId, list) {
        var nodes = [];
        for (var i = 0, l; l = list[i]; i++) {
            if (l.SecResourcesId === parentId) {
                // list.splice(i, 1); i--;
                nodes.push({
                    Id: l.Id
                  , SecResourceId: l.SecResourcesId
                  , Name: l.Name
                  , DisplayName: l.DisplayName
                  , items: createTreeMap(l.Id, list)
                });
            }
        }
        return nodes;
    }//end of createTreeMap

    function createTreeMapForApproval(parentId, list) {
        var nodes = [];
        var obj = new Object();
       
        obj = menuData[4];//4 for approval
        
        // approval process
        for (var i = 0, p; p = list[i]; i++) {
            if (p.SecResourcesId === parentId) {                
                obj.items.push({
                    Id: p.Id
                  , SecResourceId: p.SecResourcesId
                  , Name: p.Name
                  , DisplayName: p.DisplayName
                  , items: []
                });

            }
        }

        //process level
        var objLevel = new Object();
        var levelArray = [];
        for (var i = 0, l; l = obj.items[i]; i++) {
           
            objLevel = obj.items[i];
            for (var j = 0; j < list.length; j++) {
                if (list[j].SecResourcesId == l.Id) {
                    
                    objLevel.items.push(list[j]);


                }

            }
            
        }
    }



});


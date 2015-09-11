(function () {

    app.controller('PromotionalOfferController', function ($scope, $http, Erpmodal) {
        $scope.Regions = new Object();
        $scope.Resources = new Object();
        $scope.Categories = new Object();
        var SavedOfferId = 0;
        var state = 0;

        $scope.OfferState = false;
        $scope.ButtonDisabled = true;
        $scope.PromoOffer = new Object();

        var init = function () {

            $scope.OfferState = false;
            $scope.ButtonDisabled = true;            

            $scope.Regions = new Object();
            $scope.Resources = new Object();
            $scope.Categories = new Object();
            SavedOfferId = 0;
            state = 0;

            //Get all promotional offers
            $http({
                url: '/Sales/PromotionalOffers/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Resources = data;
            });
            //Get All Regions
            $http({
                url: '/Sales/Region/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Regions = data;
            });

            //Get All Categories
            $http({
                url: '/Sales/PromotionalOffers/GetAllCategoriesForOffer',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                $scope.Categories = data;
            });


            //$scope.Categories = [
            //    { IsOffered: false, CategoryName: "Abc", Discount: 5 },
            //    { IsOffered: true, CategoryName: "Pqr", Discount: 4 },
            //    { IsOffered: false, CategoryName: "Def", Discount: 7 }
            //];


        };

        init();

        $scope.selectedRow = null;  // initialize our variable to null
        $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
            $scope.selectedRow = index;
        }

        $scope.setFortEdit = function (pid) {
            state = 1;
            SavedOfferId = pid.Id;

            $scope.PromoOffer.Title = pid.Title;
            $scope.PromoOffer.StartDate = ConverttoDateString(pid.StartDate);
            $scope.PromoOffer.EndDate = ConverttoDateString(pid.EndDate);
            $scope.PromoOffer.Remarks = pid.Remarks;
            $scope.PromoOffer.CreatedBy = pid.CreatedBy;
            $scope.PromoOffer.CreatedDate = ConverttoDateString(pid.CreatedDate);

            $scope.ButtonDisabled = false;
            $scope.PromoOffer.RegionId = pid.SlsRegionId;
            $scope.Categories = pid.OfferCategories;

            $scope.ButtonDisabled = false;
            $scope.OfferState = true;

        };

        $scope.Save = function (isValid) {
            if (isValid)
                Erpmodal.Confirm('Save').then(function (result) {
                    if (state == 1) {
                        $scope.PromoOffer.Id = SavedOfferId;
                    }
                    else { $scope.PromoOffer.Id = 0; }

                    $scope.PromoOffer.SlsRegionId = $scope.PromoOffer.RegionId;
                    $scope.PromoOffer.OfferCategories = $scope.Categories;
                    
                    $http.post("/Sales/PromotionalOffers/Save", $scope.PromoOffer).success(
                        function (data) {
                            Erpmodal.Save(data, "Save");
                            $scope.Reset();
                        }
                        ).error(function (error) {

                        });
                });


        };//end of Save

        $scope.Reset = function () {
            init();
            $scope.PromoOffer = {};
            $scope.PromoOfferForm.$setPristine();
        }//end of Reset

    });

}).call(this);
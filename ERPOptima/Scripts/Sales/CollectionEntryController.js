
(function () {

    app.controller('CollectionEntry', function ($scope, $http, Erpmodal) {

        var state;
        $scope.CollectionEntry = new Object();
        $scope.RefNo = new Object();

        $scope.Resources = new Object();

        $scope.collectedFrom = new Object();
        //var collectedFromId = 0;
        var CollectionEntryId;
        $scope.Parties = new Object();
        $scope.PartyDetails = [];

        $scope.CollectionEntry.PartyType = 0;

    

        var init = function () {
            
            $scope.ButtonDisabled = true;
            state = 0;

            CollectionEntryId = 0;
            //RefNo

            $http({
                url: '/Sales/Collection/GetAutoNumber',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {
                
                $scope.CollectionEntry.RefNo = data.Refno;
            });

            $http({
                url: '/Sales/Collection/GetAll',
                method: 'GET',
                headers: { 'Content-Type': 'application/json' }
            }).success(function (data) {

                $scope.Resources = data;
            });

        };//end of init

        init();

        $scope.selectedRow = null;  // initialize our variable to null
        $scope.setClickedRow = function (index) {  //function that sets the value of selectedRow to current index
            $scope.selectedRow = index;
        }

        //   Collected party

        $scope.ChangeType = function () {

            if ($scope.CollectionEntry.PartyType == 1) {
                $http({
                    url: '/Sales/Distributor/GetAll',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $scope.Parties = data;
                });

            }

            else if ($scope.CollectionEntry.PartyType == 2) {
                $http({
                    url: '/Sales/Retailer/GetAll',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $scope.Parties = data;
                });


            }
            else if ($scope.CollectionEntry.PartyType == 3) {
                $http({
                    url: '/Sales/Dealer/GetAll',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $scope.Parties = data;
                });
            }

            else if ($scope.CollectionEntry.PartyType == 4) {
                $http({
                    url: '/Sales/CorporateClient/GetAll',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' }
                }).success(function (data) {
                    $scope.Parties = data;
                });
            }

            else {
                $scope.Parties = new Object();
            }
        }

        $scope.GetPartyDetail = function () {
            if ($scope.CollectionEntry.PartyType == 1) {
                $http({
                    url: '/Sales/Distributor/GetById',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' },
                    params: { distId: $scope.partyId }
                }).success(function (data) {
                    $scope.PartyDetails.push(data);
                });




            }

            else if ($scope.CollectionEntry.PartyType == 2) {
                $http({
                    url: '/Sales/Retailer/GetById',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' },
                    params: { distId: $scope.partyId }
                }).success(function (data) {
                    $scope.PartyDetails.push(data);
                });

            }
            else if ($scope.CollectionEntry.PartyType == 3) {
                $http({
                    url: '/Sales/Dealer/GetById',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' },
                    params: { distId: $scope.partyId }
                }).success(function (data) {
                    $scope.PartyDetails.push(data);
                });

            }
            else if ($scope.CollectionEntry.PartyType == 4) {
                $http({
                    url: '/Sales/CorporateClient/GetById',
                    method: 'GET',
                    headers: { 'Content-Type': 'application/json' },
                    params: { distId: $scope.partyId }
                }).success(function (data) {
                    $scope.PartyDetails.push(data);
                });

            }

        }

        
        //Save
        $scope.Save = function () {
                       
                Erpmodal.Confirm('Save').then(function (result) {
                    
                    if (state == 1) {
                        $scope.CollectionEntry.Id = CollectionEntryId;
                    }
                    else {
                        $scope.CollectionEntry.Id = 0;
                    }
                //    $scope.CollectionEntry.SlsCollection.CollectedFrom = $scope.SlsCollection.CollectedFrom;

                    $http.post("/Sales/Collection/Save", $scope.CollectionEntry).success(
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
            $scope.ButtonDisabled = true;
            state = 0;
            $scope.CollectionEntry = {};

            $scope.partyId = 0;

            $scope.Parties = new Object();
            $scope.CollectionEntry.PartyType = 0;

            $scope.PartyDetails = {};
            $scope.CollectionEntry.$setPristine();
        }//end of Reset

        
        $scope.setFortEdit = function (record) {
            state = 1;
            
            $scope.CollectionEntry.PartyType = 0;
                         //  $scope.RouteSetup = jQuery.extend(true, {}, record);

            CollectionEntryId = record.Id;

            $scope.CollectionEntry.CreatedBy = record.CreatedBy;

            $scope.CollectionEntry.RefNo = record.RefNo
            $scope.CollectionEntry.PaymentMode = record.PaymentMode;
            $scope.CollectionEntry.PartyType = record.PartyType;
            $scope.ChangeType();
            $scope.CollectionEntry.CollectedFrom = record.CollectedFrom;
            $scope.CollectionEntry.Amount = record.Amount;
            $scope.CollectionEntry.TransactionType = record.TransactionType;
            $scope.CollectionEntry.TransactionRefNo = record.TransactionRefNo;
            $scope.CollectionEntry.BankName = record.BankName;

        //  $scope.CollectionEntry = record;


            $scope.CollectionEntry.CreatedDate = ConverttoDateString(record.CreatedDate);
            $scope.ButtonDisabled = false;

        }
      


    });

}).call(this);

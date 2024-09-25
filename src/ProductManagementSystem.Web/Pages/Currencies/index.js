$(function () {
    var l = abp.localization.getResource("ProductManagementSystem");
	var currencyService = window.productManagementSystem.controllers.currencies.currency;
	
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Currencies/CreateModal",
        scriptUrl: abp.appPath + "Pages/Currencies/createModal.js",
        modalClass: "currencyCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Currencies/EditModal",
        scriptUrl: abp.appPath + "Pages/Currencies/editModal.js",
        modalClass: "currencyEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            code: $("#CodeFilter").val(),
			numericMin: $("#NumericFilterMin").val(),
			numericMax: $("#NumericFilterMax").val(),
			name: $("#NameFilter").val(),
			symbol: $("#SymbolFilter").val(),
			country: $("#CountryFilter").val(),
            active: (function () {
                var value = $("#ActiveFilter").val();
                if (value === undefined || value === null || value === '') {
                    return '';
                }
                return value === 'true';
            })(),
			orderMin: $("#OrderFilterMin").val(),
			orderMax: $("#OrderFilterMax").val()
        };
    };
    
    //<suite-custom-code-block-1>
    //</suite-custom-code-block-1>
    
    var dataTableColumns = [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('ProductManagementSystem.Currencies.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('ProductManagementSystem.Currencies.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    currencyService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.success(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reloadEx();;
                                        });
                                }
                            }
                        ]
                },
                width: "1rem"
            },
			{ data: "code" },
			{ data: "numeric" },
			{ data: "name" },
			{ data: "symbol" },
			{ data: "country" },
            {
                data: "active",
                render: function (active) {
                    return active ? '<i class="fa fa-check"></i>' : '<i class="fa fa-times"></i>';
                }
            },
			{ data: "order" }        
    ];
    
    
    
    

    var dataTable = $("#CurrenciesTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(currencyService.getList, getFilter),
        columnDefs: dataTableColumns
    }));
    
    
    
    //<suite-custom-code-block-2>
    //</suite-custom-code-block-2>

    createModal.onResult(function () {
        dataTable.ajax.reloadEx();;
        
        
    });

    editModal.onResult(function () {
        dataTable.ajax.reloadEx();;
        
                
    });

    $("#NewCurrencyButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reloadEx();;
        
        
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        currencyService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/app/currencies/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'code', value: input.code },
                            { name: 'numericMin', value: input.numericMin },
                            { name: 'numericMax', value: input.numericMax }, 
                            { name: 'name', value: input.name }, 
                            { name: 'symbol', value: input.symbol }, 
                            { name: 'country', value: input.country }, 
                            { name: 'active', value: input.active },
                            { name: 'orderMin', value: input.orderMin },
                            { name: 'orderMax', value: input.orderMax }
                            ]);
                            
                    var downloadWindow = window.open(url, '_blank');
                    downloadWindow.focus();
            }
        )
    });

    $('#AdvancedFilterSectionToggler').on('click', function (e) {
        $('#AdvancedFilterSection').toggle();
    });

    $('#AdvancedFilterSection').on('keypress', function (e) {
        if (e.which === 13) {
            dataTable.ajax.reloadEx();
            
            
        }
    });

    $('#AdvancedFilterSection select').change(function() {
        dataTable.ajax.reloadEx();
        
        
    });
    
    //<suite-custom-code-block-3>
    //</suite-custom-code-block-3>
    
    
    
    
    
    
    
    
});

$(function () {
    var l = abp.localization.getResource("ProductManagementSystem");
	var productService = window.productManagementSystem.controllers.products.product;
	
	
        var lastNpIdId = '';
        var lastNpDisplayNameId = '';

        var _lookupModal = new abp.ModalManager({
            viewUrl: abp.appPath + "Shared/LookupModal",
            scriptUrl: abp.appPath + "Pages/Shared/lookupModal.js",
            modalClass: "navigationPropertyLookup"
        });

        $('.lookupCleanButton').on('click', '', function () {
            $(this).parent().find('input').val('');
        });

        _lookupModal.onClose(function () {
            var modal = $(_lookupModal.getModal());
            $('#' + lastNpIdId).val(modal.find('#CurrentLookupId').val());
            $('#' + lastNpDisplayNameId).val(modal.find('#CurrentLookupDisplayName').val());
        });
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Products/CreateModal",
        scriptUrl: abp.appPath + "Pages/Products/createModal.js",
        modalClass: "productCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Products/EditModal",
        scriptUrl: abp.appPath + "Pages/Products/editModal.js",
        modalClass: "productEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            name: $("#NameFilter").val(),
			code: $("#CodeFilter").val(),
			priceMin: $("#PriceFilterMin").val(),
			priceMax: $("#PriceFilterMax").val(),
			quantityMin: $("#QuantityFilterMin").val(),
			quantityMax: $("#QuantityFilterMax").val(),
			currencyId: $("#CurrencyIdFilter").val()
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
                                visible: abp.auth.isGranted('ProductManagementSystem.Products.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.product.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('ProductManagementSystem.Products.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    productService.delete(data.record.product.id)
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
			{ data: "product.name" },
			{ data: "product.code" },
			{ data: "product.price" },
			{ data: "product.quantity" },
            {
                data: "currency.name",
                defaultContent : ""
            }        
    ];
    
    
    
    

    var dataTable = $("#ProductsTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(productService.getList, getFilter),
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

    $("#NewProductButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reloadEx();;
        
        
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        productService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/app/products/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'name', value: input.name }, 
                            { name: 'code', value: input.code },
                            { name: 'priceMin', value: input.priceMin },
                            { name: 'priceMax', value: input.priceMax },
                            { name: 'quantityMin', value: input.quantityMin },
                            { name: 'quantityMax', value: input.quantityMax }, 
                            { name: 'currencyId', value: input.currencyId }
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

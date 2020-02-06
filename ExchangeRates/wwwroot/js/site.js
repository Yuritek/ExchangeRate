$(function () {
    //$('#datetimepicker1').datetimepicker({
    //    locale: 'ru',
    //    format: 'DD.MM.YYYY'
    //}).on('dp.change',
    //    function() {
    //        $(this).data("DateTimePicker").hide();
    //    });
    //$('#datetimepicker2').datetimepicker({
    //    locale: 'ru',
    //    useCurrent: false,
    //    format: 'DD.MM.YYYY',
    //    maxDate: 'now'
    //}).on('dp.change',
    //    function () {
    //        $(this).data("DateTimePicker").hide();
    //    });
    //$('#datetimepickerRemove1').click(function () {
    //    $('#datetimepicker1').data("DateTimePicker").clear();
    //});
    //$('#datetimepickerRemove2').click(function () {
    //    $('#datetimepicker2').data("DateTimePicker").clear();
    //});
});

function DrawChart(data) {
    var chartName = "chart";
    var ctx = document.getElementById(chartName).getContext('2d');
    var minValue = parseInt(data[0].minYValues);
    var data = {
        labels: JSON.parse(data[0].xLabels),
        datasets: [{
            label: data[0].title,
            backgroundColor: 'rgb(255, 0, 0)',
            borderColor: 'rgb(255, 0, 0)',
            borderWidth: 1,
            data: JSON.parse(data[0].yValues)
                .map(function(name) {
                    return name.replace(',', '.');
                }),
            fill: false
        },
            {
            label:data[1].title,
                backgroundColor: 'rgb(0, 0, 255)',
                borderColor: 'rgb(0, 0, 255)',
            borderWidth: 1,
                data: JSON.parse(data[1].yValues)
                    .map(function(name) {
                        return name.replace(',', '.');
                    }),
            fill: false
            }
        ]
    };
    var options = {
        maintainAspectRatio: false,
        scales: {
            yAxes: [{
                ticks: {
                    min: minValue,
                    beginAtZero: true,
                    stepSize: 1
                },
                gridLines: {
                    display: true,
                    color: "rgba(255,99,164,0.2)"
                }
            }],
            xAxes: [{
                gridLines: {
                    display: true
                }
            }]
        }
    };
    var myChart = new Chart(ctx, {
        options: options,
        data: data,
        type: 'line'

    });
}

$("#reset").click(function (e) {
    e.preventDefault();
	var dict2 = {
		"1": {
			type: "Мост",
			attr: ["Field1", "Field2"]
	    },
	    "2": {
		   type: "Туннель",
		   attr: ["Field1", "Field2"]
	    }
    };

    var dicU = dict2["1"];
	var dicT = dicU.type;
    var dict = {};
    dict["Name"] = "Мост через реку";
    $('#GisModal').find('#NameHeader').text(dict["Name"]);
    var s = Object.keys(dict);
    for (var i=0; i<s.length;i++) {
	   var key = s[i];
	   var newKey = "#Gis".concat(key);
	   var e = $('#GisModal').find(newKey);
	   if (e) {
		   var inputValue = "#GisInput".concat(key);
		  $('#GisModal ' + inputValue).val(dict["Name"]);
		  $('#GisModal ' + inputValue).prop('disabled', false);
		  e.removeClass("d-none");
	   }
    }
	$('#GisModal').modal('show');
	//var datePicker1 = $('#datetimepicker1').data("DateTimePicker").date();
	//var datePicker2 = $('#datetimepicker2').data("DateTimePicker").date();
	//if (datePicker1 == null || datePicker2== null) {
	//    alert('Укажите полный интервал даты');
	//}
	//var data = {
	//    DateBegin: datePicker1.format('DD.MM.YYYY'),
	//    DateEnd: datePicker2.format('DD.MM.YYYY')
	//};
	//GetExchangeRate(data);
});

function GetExchangeRate(parametr) {
    $.ajax({
        type: 'POST',
        url: "api/CurrencyRate",
        contentType: 'application/json',
        async:'false',
        data: JSON.stringify(parametr),
        success: function (data) {
            DrawChart(data);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Произошла ошибка при получении данных");
        }
    });
}
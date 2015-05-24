




sabio.services.recruiters.add = function (recruiterData, onSuccess, onError) {
	
    //console.log(recuiterData);

	//get value of #RecruiterUID and if value != null && value != ''
	//then I want to perform an update ("PUT") rather than an insert ("POST")
	//var type = "POST" or "PUT"

	var url = "/api/Recruiters";
	var settings = {
		cache: false
		, contentType: "application/x-www-form-urlencoded; charset=UTF-8"
		, data: recruiterData
		, dataType: "json"
		, success: onSuccess
		, error: onError
		, type: "POST"
	};

	$.ajax(url, settings);
};

sabio.services.recruiters.get = function ( onSuccess, onError) {
	var url = "/api/recruiters/" 
	var settings = {
		cache: false
	   , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
	   , data: null
	   , dataType: "json"
	   , success: onSuccess
	   , error: onError
	   , type: "GET"
	};
	$.ajax(url, settings);
};

sabio.services.recruiters.uidUpdate = function (hiddenUid, myData, onSuccess, onError) {

	var url = "/api/recruiters/" + hiddenUid;
	var settings = {
		cache: false
	   , contentType: "application/x-www-form-urlencoded; charset=UTF-8"
	   , data: myData
	   , dataType: "json"
	   , success: onSuccess
	   , error: onError
	   , type: "PUT"
	};
	$.ajax(url, settings);
};



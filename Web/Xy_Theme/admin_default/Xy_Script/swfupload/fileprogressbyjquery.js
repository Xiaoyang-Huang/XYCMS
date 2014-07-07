// JavaScript Document

function FillProgressByJquery(file , fileHandler){
	var fileProgressID = file.id;
	var opacity = 100;
	var height = 0;

	var fileProgressWrapper;
	var progressCancel;
	var progressText;
	var progressContent;
	var progressBarWrap;
	var progressBar;
	var progressStatus;

	this.reset = function () {
	    fileProgressWrapper.attr('class', 'progressWrapper');
	    progressStatus.attr('class', 'progressBarStatus').html('Ready');
	    progressBar.attr('class', 'progressBarInProgress').css('width', '0%');
	    progressCancel.unbind();
	    var thisInEvent = this;
	    progressCancel.click(function (e) {
	        thisInEvent.cancelUpload(fileHandler);
	    })
	    fileProgressWrapper.fadeIn();
	    this.setStatus("Pending...");
	}

	this.setProgress = function (percentage) {
	    fileProgressWrapper.attr('class', 'progressWrapper process');
	    progressBar.attr('class', 'progressBarInProgress')
	    progressBar.css('width', percentage + '%');
	    fileProgressWrapper.fadeIn();
	    this.setStatus("Uploading...");
	}

	this.setError = function () {
	    fileProgressWrapper.attr('class', 'progressWrapper error');
	    progressBar.attr('class', 'progressBarError')
	    progressBar.css('width', '100%');
	    progressCancel.unbind();
	    var thisInEvent = this;
	    progressCancel.click(function (e) {
	        thisInEvent.hideUpload();
	    })
	    //setTimeout(function () { fileProgressWrapper.fadeOut() }, 5000);
	}
	this.setCancelled = function () {
	    fileProgressWrapper.attr('class', 'progressWrapper');
	    progressBar.attr('class', 'progressBarError')
	    progressBar.css('width', '100%');
	    setTimeout(function () { fileProgressWrapper.fadeOut() }, 2000);
	}

	this.setStatus = function (status) {
	    progressStatus.html(status)
	}

	this.finish = function (filename, setDelete, fileobj) {
	    fileProgressWrapper.attr('class', 'progressWrapper complete');
	    progressBar.attr('class', 'progressBarComplete')
	    progressBar.css('width', '100%');
	    progressBarWrap.fadeOut();
	    this.setStatus("Complete.");
	    progressContent.html('<img src="' + filename + '" />');

	    progressCancel.unbind();
	    var thisInEvent = this;
	    progressCancel.click(function (e) {
	        thisInEvent.deleteUpload(fileHandler, filename, e);
	    })
	}

	this.Otherfinish = function (IconPath, filename, setDelete, fileobj) {
	    fileProgressWrapper.attr('class', 'progressWrapper complete');
	    progressBar.attr('class', 'progressBarComplete')
	    progressBar.css('width', '100%');
	    progressBarWrap.fadeOut();
	    this.setStatus("Complete.");
	    progressContent.html('<img src="' + IconPath + '" />');

	    progressCancel.unbind();
	    var thisInEvent = this;
	    progressCancel.click(function (e) {
	        thisInEvent.deleteUpload(fileHandler, filename, e);
	    })
	}

	this.cancelUpload = function (fileHandler) {
	    fileHandler.cancelUpload(fileProgressID);
	    fileProgressWrapper.remove();
	}

	this.hideUpload = function (e) {
	    fileProgressWrapper.remove();
	}

	this.deleteUpload = function (fileHandler, filename, evt) {
	    //var imgpath = $(evt.target).siblings('.progressContent').find('img').attr('src');
	    //var imgfile = imgpath.substr(imgpath.lastIndexOf('/') + 1);
	    var imgfile = filename.substring(filename.lastIndexOf('/') + 1);
	    var oldpaths = getControlByClassOrId(fileHandler.customSettings.valueControl).val().split(fileHandler.customSettings.splitSymbol);
	    var newpaths = new Array();


	    var imgpath = $(evt.target).siblings('.progressContent').find('img').attr('src');
	    var imgfile = imgpath.substr(imgpath.lastIndexOf('/') + 1);
	    var oldpaths = getControlByClassOrId(fileHandler.customSettings.valueControl).val().split(fileHandler.customSettings.splitSymbol);
	    var newpaths = new Array();
	    $(oldpaths).each(function (i, o) {
	        if (o != imgfile) {
	            newpaths.push(o);
	        }
	    })

	    getControlByClassOrId(fileHandler.customSettings.valueControl).val('');
	    $(newpaths).each(function (i, o) {
	        if (getControlByClassOrId(fileHandler.customSettings.valueControl).val().length > 0) {
	            getControlByClassOrId(fileHandler.customSettings.valueControl).val(getControlByClassOrId(fileHandler.customSettings.valueControl).val() + fileHandler.customSettings.splitSymbol + o);
	        } else {
	            getControlByClassOrId(fileHandler.customSettings.valueControl).val(o);
	        }
	    })

	    fileProgressWrapper.remove();
	}

	
	if ($('#' + fileProgressID).length == 0) {
	    fileProgressWrapper = $('<div id="' + fileProgressID + '" class="progressWrapper"></div>');
	    progressCancel = $('<a class="progressCancel" title="删除此照片">删除</a>');
	    progressText = $('<div class="progressName"></div>').html(file.name);
	    progressContent = $('<div class="progressContent"></div>');
	    progressBarWrap = $('<div class="progressBarInProgressWrap"></div>');
	    progressBar = $('<div class="progressBarInProgress"></div>');
	    progressStatus = $('<div class="progressBarStatus"></div>');


	    progressBarWrap.append(progressBar);
	    fileProgressWrapper.append(progressText);
	    fileProgressWrapper.append(progressContent);
	    fileProgressWrapper.append(progressBarWrap);
	    fileProgressWrapper.append(progressCancel);
	    fileProgressWrapper.append(progressStatus);

	    getControlByClassOrId(fileHandler.customSettings.processTarget).append(fileProgressWrapper);
	    this.reset();
	} else {
	    fileProgressWrapper = $('#' + fileProgressID);
	    progressCancel = fileProgressWrapper.find('.progressCancel');
	    progressText = fileProgressWrapper.find('.progressName');
	    progressContent = fileProgressWrapper.find('.progressContent');
	    progressBarWrap = fileProgressWrapper.find('.progressBarInProgressWrap');
	    progressBar = fileProgressWrapper.find('.progressBarInProgress');
	    progressStatus = fileProgressWrapper.find('.progressBarStatus');
	}

	function getControlByClassOrId(idorclass) {
	    return $('.' + idorclass).size() == 0 ? $('#' + idorclass) : $('.' + idorclass);
	}
}
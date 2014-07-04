/* ==========================================================
 * ErgoAdmin v1.0
 * widgets.js
 * 
 * http://www.mosaicpro.biz
 * Copyright MosaicPro
 *
 * ========================================================== */ 

$(function()
{
	$('#widget-progress-bar .bar').width("50%");
	setInterval(function(){
		var w = mt_rand(30, 100);
		$('#widget-progress-bar .steps-percent').html(w + "%");
		$('#widget-progress-bar .bar').width(w + "%");
	}, 2000);
});
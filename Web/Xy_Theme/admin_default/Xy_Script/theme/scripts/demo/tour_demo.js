/* ==========================================================
 * ErgoAdmin v1.0
 * tour_demo.js
 * 
 * http://www.mosaicpro.biz
 * Copyright MosaicPro
 *
 * ========================================================== */ 

$(function()
{
	if (!$('#tlyPageGuide').length)
		return false;
	
	tl.pg.init({
		custom_open_button: '#tour-demo-start'
	});
	
	setTimeout(function(){
		$('#tour-demo-start').click();
	}, 1000);

});
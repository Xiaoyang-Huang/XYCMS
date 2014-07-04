/* ==========================================================
 * ErgoAdmin v1.0
 * gallery_2.js
 * 
 * http://www.mosaicpro.biz
 * Copyright MosaicPro
 *
 * ========================================================== */ 

function masonryGallery()
{
	var $container = $('.gallery-masonry ul');
	$container.each(function()
	{
		var c = $(this);
		
		if (c.is('.masonry'))
			c.masonry('reload');

		c.imagesLoaded( function()
		{
			c.masonry({
				gutterWidth: 9,
		    	itemSelector : 'li',
		    	columnWidth: c.find('li:first').width(),
		    	isAnimated: true,
		    	animationOptions: {
		    		duration: 250,
		    	    easing: 'linear',
		    	    queue: true
		    	}
		  	});
		});
	});
}

$(function()
{
	masonryGallery();

	$(window).resize(function(e){
		masonryGallery();
	});
});
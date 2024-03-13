// Get the content from the element
var content = document.getElementById('content');

// Regular expression to find URLs
var urlRegex = /(https?:\/\/[^\s]+)/g;

// Replace URLs with clickable links
var formattedContent = content.innerHTML.replace(urlRegex, function (url) {
    return '<a class="link-underline" href="' + url + '" target="_blank">' + url + '</a>';
});

// Update the content in the element
content.innerHTML = formattedContent;
function convertToShortDate(dateString) {
    const shortDate = new Date(dateString).toLocaleDateString('en-US');
    return shortDate;
}
function convertFirstLetterUpperCase(text) {
    return text.charAt(0).toUpperCase() + text.slice(1);
}
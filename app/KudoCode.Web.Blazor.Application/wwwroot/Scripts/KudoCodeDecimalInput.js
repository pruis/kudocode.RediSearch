window.KudoCodeDecimalInput = {
    addEventListener: function (id) {
        var element = document.getElementById(id);

        onLoad(element);
        element.addEventListener('focus', onFocus);
        element.addEventListener('blur', onBlur);

        function onFocus(e) {
            var value = e.target.value;
            e.target.value = value ? localStringToNumber(value) : '';
        }

        function onBlur(e) {
            var value = e.target.value;

            e.target.value = value
                ? localStringToNumber(value)
                : '';
        }

        function onLoad(element) {
            var x = element.value
                ? localStringToNumber(element.value)
                : '';

            $(element).val(x);
        }

        function localStringToNumber(s) {
            return numeral(s).format('0,0.00');
        }
    }
};
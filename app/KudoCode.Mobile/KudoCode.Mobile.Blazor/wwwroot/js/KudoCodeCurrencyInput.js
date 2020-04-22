window.KudoCodeCurrencyInput = {
    addEventListener: function (id) {
        var element = document.getElementById(id);
        var currency = 'ZAR';

        onLoad(element);
        element.addEventListener('focus', onFocus);
        element.addEventListener('blur', onBlur);

        function onFocus(e) {
            var value = e.target.value;
            e.target.value = value ? localStringToNumber(value) : '';
        }

        function onBlur(e) {
            var value = e.target.value;

            var options = {
                maximumFractionDigits: 2,
                currency: currency,
                style: "currency",
                currencyDisplay: "symbol"
            };

            var x = value
                ? localStringToNumber(value).toLocaleString(undefined, options)
                : '';

            e.target.value = x.replace('ZAR', 'R');
        }

        function onLoad(element) {

            var options = {
                maximumFractionDigits: 2,
                currency: currency,
                style: "currency",
                currencyDisplay: "symbol"
            };


            var x = element.value
                ? localStringToNumber(element.value).toLocaleString(undefined, options)
                : '';

            x = x.replace('ZAR', 'R');

            $(element).val(x);
        }

        function localStringToNumber(s) {
            return Number(String(s).replace(/[^0-9.-]+/g, ""));
        }
    }
};
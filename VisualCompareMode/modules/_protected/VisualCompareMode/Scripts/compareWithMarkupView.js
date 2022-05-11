define([
    "dojo/_base/declare",
    "dojo/_base/lang",
    "dojo/topic",
    "epi/shell/widget/Iframe",
    "epi-cms/compare/views/SideBySideCompareView",
    "epi/shell/_ContextMixin",
    "epi/routes"
],

function (
    declare,
    lang,
    topic,
    Iframe,
    SideBySideCompareView,
    _ContextMixin,
    routes
) {

    return declare([SideBySideCompareView, _ContextMixin], {

        startup: function() {
            this.subscribe("/epi/shell/action/viewchanged", this._contentUpdated);
        },

        _setRightVersionUrlAttr: function (url) {

            var getDiffUrl = this._getDiffUrl();

            var right;
            if (this.model.rightVersion == null) {
                // Only one version exists to make left version = right version
                right = this.model.leftVersion;
            } else {
                right = this.model.rightVersion;
            }

            try {
                this._rightIframe.load(
                    getDiffUrl +
                    "?ts=" + this._getTimestamp() +
                    "&first=" + this.model.leftVersion.contentLink +
                    "&second=" + right.contentLink +
                    "&firstlang=" + this.model.leftVersion.language +
                    "&secondlang=" + right.language);
            } catch (ex) {
                console.log(ex);
            }
        },

        _getDiffUrl: function () {
            var getDiffUrl = routes.getActionPath({ moduleArea: "VisualCompareMode", controller: "VisualCompare"});

            return getDiffUrl;
        },

        _getLeftUrl: function () {
        },

        _getRightUrl: function () {
        },

        _setupComparePanes: function() {
            this.inherited(arguments);
            var orientation = this.model.orientation;
            this._rightIframe.set('style', orientation === "vertical" ? "width: 100%;" : "height: 100%");
            this.mainLayoutContainer.layout();
        },

        _getTimestamp: function () {
            var currentdate = new Date();
            var datetime = currentdate.getDate() + "." +
                (currentdate.getMonth() + 1) + "." +
                currentdate.getFullYear() + "." +
                currentdate.getHours() + "." +
                currentdate.getMinutes() + "." +
                currentdate.getSeconds();
            return datetime;
        },

        _contentUpdated: function (ctx, callerData) {
            this.set('RightVersionUrl', '');
        },
        
        contextChanged: function (ctx, callerData) {
            this.set('RightVersionUrl', '');
        }

    });
});

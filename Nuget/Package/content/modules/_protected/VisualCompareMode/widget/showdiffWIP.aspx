<%@ Page language="C#" %>
<%@ Import Namespace="EPiServer.Configuration" %>

<!DOCTYPE html>
<script language="javascript" type="text/javascript">
    (function() {
        // Load the script
        var script = document.createElement("SCRIPT");
        script.src = 'https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js';
        script.type = 'text/javascript';
        script.onload = function() {

            function getUrlVars() {
                var vars = [], hash;
                var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
                for (var i = 0; i < hashes.length; i++) {
                    hash = hashes[i].split('=');
                    vars.push(hash[0]);
                    vars[hash[0]] = hash[1];
                }
                return vars;
            }

            var rootUrl = "/<%=Settings.Instance.UIUrl.OriginalString.TrimStart("~/".ToCharArray()).TrimEnd("/".ToCharArray())%>/"; 
            var $ = window.jQuery;
            var version1;
            var version2;

            $.ajax({
                url: rootUrl +
                    "Content/" +
                    getUrlVars()["secondlang"] +
                    "/,," +
                    getUrlVars()["second"] +
                    "/?epieditmode=False",
                context: document.body
            }).done(function(data) {
                version1 = data;
                $.ajax({
                    url: rootUrl +
                        "Content/" +
                        getUrlVars()["firstlang"] +
                        "/,," +
                        getUrlVars()["first"] +
                        "/?epieditmode=False",
                    context: document.body
                }).done(function(data2) {
                    version2 = data2;
                    var postData = "version1=" + encodeURIComponent(data) + "&version2=" + encodeURIComponent(data2);
                    $.post(rootUrl + "VisualCompare/",
                        postData,
                        function(data3) {
                            document.open();
                            document.write(data3);
                            document.close();
                        });
                });
            });
        };
        document.getElementsByTagName("head")[0].appendChild(script);
    })();
</script>
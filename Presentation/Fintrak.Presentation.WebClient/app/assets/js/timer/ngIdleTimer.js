angular.module('ngIdleTimer',[])

    .factory('ngIdleTimer', function($compile,$rootScope,$document) {
        return {
            onIdleTimeOut: function(func,timeout){
                var idleTime = 0;
                $document.ready(function () {
                    var idleInterval = setInterval(timerIncrement, 3600);
                    $document.bind('mousemove',function (e) {
                        idleTime = 0;
                    });
                    $document.bind('keypress',function (e) {
                        idleTime = 0;
                    });
                });

                function timerIncrement() {
                    idleTime = idleTime + 1;
                    if (idleTime > timeout) {
                        func();
                    }
                }
            }
        }
    });

const swBuild = require('workbox-build');

swBuild.generateSW({
    globDirectory: 'wwwroot',
    //globPatterns: [
    //    './**/*.css',
    //    './**/*.js',
    //    './**/*.{png,jpg,css}',
    //],
    globPatterns: [
        './**/*.{js,html,css,png,jpg,jpeg}',
         "../node_modules/jquery/dist/jquery.min.js",
         "../node_modules/jquery-ajax-unobtrusive/jquery.unobtrusive-ajax.min.js",
         "../node_modules/jquery-validation/dist/jquery.validate.min.js",
         "../node_modules/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js",
         "../node_modules/material-components-web/dist/material-components-web.min.js",
         "../node_modules/material-design-lite/material.min.js"
    ],
    resolve: {
        modules: ['node_modules'],
        extensions: ['.js', '.json', '.jsx', '.css']
    },
    swDest: 'wwwroot/sw.js',
     runtimeCaching: [
       {
             urlPattern: /.*fonts\.(gstatic|googleapis)\.com\.*/,
         handler: 'cacheFirst',
         options: {
           cacheName: 'google',
           cacheExpiration: {
             maxEntries: 100
           }
         }
       }
     ]
}).then(() => console.log('Service Worker generated')).catch(err => console.error(err, 'Service Worker failed to generate'));
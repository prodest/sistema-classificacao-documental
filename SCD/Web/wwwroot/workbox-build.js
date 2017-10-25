const swBuild = require('workbox-build');

swBuild.generateSW({
    globDirectory: 'wwwroot',
    globPatterns: [
        '**/*.css',
        '**/*.js',
        '**/*.{png,jpg,css}',
    ],
    swDest: 'wwwroot/sw.js',
     runtimeCaching: [
       {
             urlPattern: /.*fonts\.(gstatic|googleapis)\.com\.*/,
         handler: 'cacheFirst',
         options: {
           cacheName: 'google',
           cacheExpiration: {
             maxEntries: 10
           }
         }
       }
     ]
}).then(() => console.log('Service Worker generated')).catch(err => console.error(err, 'Service Worker failed to generate'));
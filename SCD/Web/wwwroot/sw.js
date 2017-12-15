importScripts('workbox-sw.prod.v2.1.2.js');

/**
 * DO NOT EDIT THE FILE MANIFEST ENTRY
 *
 * The method precache() does the following:
 * 1. Cache URLs in the manifest to a local cache.
 * 2. When a network request is made for any of these URLs the response
 *    will ALWAYS comes from the cache, NEVER the network.
 * 3. When the service worker changes ONLY assets with a revision change are
 *    updated, old cache entries are left as is.
 *
 * By changing the file manifest manually, your users may end up not receiving
 * new versions of files because the revision hasn't changed.
 *
 * Please use workbox-build or some other tool / approach to generate the file
 * manifest which accounts for changes to local files and update the revision
 * accordingly.
 */
const fileManifest = [
  {
    "url": "css/site.css",
    "revision": "883a583013340b12445c76194e12b76c"
  },
  {
    "url": "css/site.min.css",
    "revision": "e6dff4e8cdb2dd72804d1dc037d7fc54"
  },
  {
    "url": "images/Brasao_Governo_horizontal_white_left_small.png",
    "revision": "0d229b7da42a1f72d78df74f18c069c2"
  },
  {
    "url": "images/silhouette.png",
    "revision": "96fff529ef2f7529eae27f9206ad9347"
  },
  {
    "url": "js/site.js",
    "revision": "910b5fc00807defdd341a056844b48bd"
  },
  {
    "url": "js/site.min.js",
    "revision": "3d386a511eda699f580a4ce226126354"
  },
  {
    "url": "sw-register.js",
    "revision": "111256f8f5f9b2f26341c84ef01aad7a"
  },
  {
    "url": "sw.js",
    "revision": "cfc6c405e9dc6adc7466609d4655a689"
  },
  {
    "url": "workbox-build.js",
    "revision": "ba00eb541ae59da32dd742cfa80e7be7"
  },
  {
    "url": "workbox-sw.prod.v2.1.2.js",
    "revision": "685d1ceb6b9a9f94aacf71d6aeef8b51"
  },
  {
    "url": "../node_modules/jquery/dist/jquery.min.js",
    "revision": "c9f5aeeca3ad37bf2aa006139b935f0a"
  },
  {
    "url": "../node_modules/jquery-ajax-unobtrusive/jquery.unobtrusive-ajax.min.js",
    "revision": "6ff30751d06af1a3395be028f4e33da9"
  },
  {
    "url": "../node_modules/jquery-validation/dist/jquery.validate.min.js",
    "revision": "93c1dd8416ac2af1850652d5b620a142"
  },
  {
    "url": "../node_modules/jquery-validation-unobtrusive/jquery.validate.unobtrusive.js",
    "revision": "d01589272c6b4497221e091a4c6fcbdf"
  },
  {
    "url": "../node_modules/material-components-web/dist/material-components-web.min.js",
    "revision": "23d7c655891cb2bc267b1fefc6a19934"
  },
  {
    "url": "../node_modules/material-design-lite/material.min.js",
    "revision": "713af0c6ce93dbbce2f00bf0a98d0541"
  }
];

const workboxSW = new self.WorkboxSW();
workboxSW.precache(fileManifest);
workboxSW.router.registerRoute(/.*fonts\.(gstatic|googleapis)\.com\.*/, workboxSW.strategies.cacheFirst({
  "cacheName": "google",
  "cacheExpiration": {
    "maxEntries": 100
  }
}), 'GET');

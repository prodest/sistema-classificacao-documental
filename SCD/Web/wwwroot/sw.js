importScripts('workbox-sw.prod.v2.0.1.js');

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
    "url": "css/material.css",
    "revision": "dbf2ea8c38b9c0384c623b7e17cbe903"
  },
  {
    "url": "css/material.cyan-light_blue.min.css",
    "revision": "2d3475d22c71831e11ae0e32e115dff7"
  },
  {
    "url": "css/material.min.css",
    "revision": "a09f24c85cb39ef5db425b71c8c98c4a"
  },
  {
    "url": "css/site.css",
    "revision": "90a1f322040e4977ac85e88f6e544dcc"
  },
  {
    "url": "css/site.min.css",
    "revision": "b5ac1ff3c958ca310dd62dc9a2299897"
  },
  {
    "url": "css/styles.css",
    "revision": "6985687e978281593f7c1c0fd54e6e87"
  },
  {
    "url": "images/jquery.validate.min.js",
    "revision": "c2e02460a0c2bb3c499009f8aa4297ab"
  },
  {
    "url": "images/messages_pt_BR.js",
    "revision": "5b68cad7b9f87b0fd37737c548b132cb"
  },
  {
    "url": "js/material.min.js",
    "revision": "e68511951f1285c5cbf4aa510e8a2faf"
  },
  {
    "url": "js/site.js",
    "revision": "9608aa4e6cc326780ba703472cf2a314"
  },
  {
    "url": "sw-register.js",
    "revision": "111256f8f5f9b2f26341c84ef01aad7a"
  },
  {
    "url": "workbox-build.js",
    "revision": "1402d5bac520f00693b870a35dae170e"
  },
  {
    "url": "images/android-desktop.png",
    "revision": "62e83746d2fb2ea78ebd7a77621b606e"
  },
  {
    "url": "images/Brasao_Governo_horizontal_white_left_small.png",
    "revision": "0d229b7da42a1f72d78df74f18c069c2"
  },
  {
    "url": "images/dog.png",
    "revision": "27d1121826606d32e7d7acca687fdcef"
  },
  {
    "url": "images/favicon.png",
    "revision": "b637902656162d60f1cf2865bd60ce84"
  },
  {
    "url": "images/ios-desktop.png",
    "revision": "4a0879d8b6d2254abdd3f42c98b838f0"
  },
  {
    "url": "images/user.jpg",
    "revision": "01b46903dd0c2cb3b0abc908f3095d93"
  }
];

const workboxSW = new self.WorkboxSW();
workboxSW.precache(fileManifest);
workboxSW.router.registerRoute(/.*fonts\.(gstatic|googleapis)\.com\.*/, workboxSW.strategies.cacheFirst({
  "cacheName": "google",
  "cacheExpiration": {
    "maxEntries": 10
  }
}), 'GET');

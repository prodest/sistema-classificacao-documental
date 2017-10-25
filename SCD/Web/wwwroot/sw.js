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
    "url": "css/site.css",
    "revision": "90a1f322040e4977ac85e88f6e544dcc"
  },
  {
    "url": "css/site.min.css",
    "revision": "b5ac1ff3c958ca310dd62dc9a2299897"
  },
  {
    "url": "js/material.min.js",
    "revision": "e68511951f1285c5cbf4aa510e8a2faf"
  },
  {
    "url": "js/site.js",
    "revision": "a0427aea5717d693e1bdde309149022b"
  },
  {
    "url": "js/site.min.js",
    "revision": "81e6cb49c26b95fa69476bc99187f16c"
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
    "url": "images/Brasao_Governo_horizontal_white_left_small.png",
    "revision": "0d229b7da42a1f72d78df74f18c069c2"
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

﻿const path = require('path');

module.exports = {
  errorEmailAddress: '',
  emailConfig: {
    email: null,
    smtpHosts: null
  },
  common: {
    assetsSubDirectory: 'static',
    productionSourceMap: false,

    // Gzip off by default as many popular static hosts such as
    // Surge or Netlify already gzip all static assets for you.
    // Before setting to `true`, make sure to:
    // npm install --save-dev compression-webpack-plugin
    productionGzip: false,
    productionGzipExtensions: ['js', 'css']
  },
  env: '"development"',
  prod: {
    env: {
      HOST: 'www.myproductionurl.com',
      NODE_ENV: '"production"',
      PORT: 80,
      PORT_HTTPS: 443
    },
    urls: {
      api: '"https://www.myproductionurl.com/api"',
      web: '"https://www.myproductionurl.com"'
    },
    ssl: {
      key: path.join(__dirname, '/server/ssl/server.key'),
      cert: path.join(__dirname, '/server/ssl/server.cert')
    }
  },
  staging: {
    env: {
      HOST: 'www.mystagingurl.com',
      NODE_ENV: '"production"',
      PORT: 80,
      PORT_HTTPS: 443
    },
    urls: {
      api: '"https://www.mystagingurl.com/api"',
      web: '"https://www.mystagingurl.com"'
    },
    ssl: {
      key: path.join(__dirname, '/server/ssl/server.key'),
      cert: path.join(__dirname, '/server/ssl/server.cert')
    }
  },
  test: {
    env: {
      HOST: 'www.testurl.com',
      NODE_ENV: '"test"',
      PORT: 80,
      PORT_HTTPS: 443
    },
    urls: {
      api: '"https://www.testurl.com/api"',
      web: '"https://www.testurl.com"'
    },
    ssl: {
      key: path.join(__dirname, '/server/ssl/server.key'),
      cert: path.join(__dirname, '/server/ssl/server.cert')
    }
  },
  dev: {
    env: {
      HOST: 'localhost',
      NODE_ENV: '"development"',
      PORT: 22976,
      PORT_HTTPS: 22976
    },
    urls: {
      api: '"http://localhost:22976"',
      web: '"http://localhost:22976/api"'
    },
    ssl: {
      key: path.join(__dirname, '/server/ssl/server.key'),
      cert: path.join(__dirname, '/server/ssl/server.cert')
    }
  },
  unittest: {
    env: {
      NODE_ENV: '"testing"'
    }
  }
};

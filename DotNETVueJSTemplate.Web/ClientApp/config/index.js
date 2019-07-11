module.exports = {
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
  prod: {
    env: {
      NODE_ENV: '"production"'
    },
    urls: {
      api: '"https://api.myproductionurl.com"',
      web: '"https://www.myproductionurl.com"'
    }
  },
  staging: {
    env: {
      NODE_ENV: '"production"'
    },
    urls: {
      api: '"https://api.mystagingurl.com"',
      web: '"https://www.mystagingurl.com"'
    }
  },
  test: {
    env: {
      NODE_ENV: '"test"'
    },
    urls: {
      api: '"https://api.testurl.com"',
      web: '"https://www.testurl.com"'
    }
  },
  dev: {
    env: {
      NODE_ENV: '"development"'
    },
    urls: {
      api: '"http://localhost:22974"',
      web: '"http://localhost:22975"'
    }
  },
  unittest: {
    env: {
      NODE_ENV: '"testing"'
    }
  }
};

﻿const webpack = require('webpack');
const merge = require('webpack-merge');
const FriendlyErrorsPlugin = require('friendly-errors-webpack-plugin');
const config = require('../config');
const baseWebpackConfig = require('./webpack.base.config');

module.exports = merge(baseWebpackConfig(config.dev.env), {
  devtool: '#inline-source-map', // inline

  plugins: [
    new webpack.DefinePlugin({
      'process.env': config.dev.env,
      '_config.urls': config.dev.urls
    }),
    new webpack.NoEmitOnErrorsPlugin(),
    new FriendlyErrorsPlugin()
  ]
});

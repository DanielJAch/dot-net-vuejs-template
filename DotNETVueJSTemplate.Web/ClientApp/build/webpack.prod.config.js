const webpack = require('webpack');
const merge = require('webpack-merge');
const UglifyJsPlugin = require('uglifyjs-webpack-plugin');
const CompressionWebpackPlugin = require('compression-webpack-plugin');
const config = require('../config');
const baseWebpackConfig = require('./webpack.base.config');

module.exports = merge(baseWebpackConfig(config.prod.env), {
	devtool: config.common.productionSourceMap ? '#source-map' : false,

  plugins: [
	  new webpack.DefinePlugin({
      'process.env': config.prod.env,
      '_config.urls': config.prod.urls
	  }),
	  new UglifyJsPlugin(),
	  new CompressionWebpackPlugin({
		  asset: '[path].gz[query]',
		  algorithm: 'gzip',
		  test: new RegExp(`\\.(${config.common.productionGzipExtensions.join('|')})$`),
		  threshold: 10240,
		  minRatio: 0.8
	  })
  ]
});

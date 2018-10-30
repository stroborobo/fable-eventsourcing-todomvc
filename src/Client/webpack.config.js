var path = require("path");
var webpack = require("webpack");
var MinifyPlugin = require("terser-webpack-plugin");

function resolve(filePath) {
    return path.join(__dirname, filePath)
}

var indexHtml = resolve("./public/index.html");

var CONFIG = {
    entry: {
        app: [
            "whatwg-fetch",
            "@babel/polyfill",
            resolve("./Client.fsproj")
        ],
        // index: indexHtml,
    },
    // devServerProxy: {
    //     '/api/*': {
    //         target: 'http://localhost:' + (process.env.SUAVE_FABLE_PORT || "8085"),
    //         changeOrigin: true
    //     }
    // },
    historyApiFallback: {
        index: indexHtml
    },
    contentBase: resolve("./dist"),
    // Use babel-preset-env to generate JS compatible with most-used browsers.
    // More info at https://github.com/babel/babel/blob/master/packages/babel-preset-env/README.md
    babel: {
        presets: [
            ["@babel/preset-env", {
                "targets": {
                    "browsers": ["last 2 versions"]
                },
                "modules": false,
                "useBuiltIns": "usage",
            }]
        ],
        plugins: ["@babel/plugin-transform-runtime"]
    }
}

var isProduction = process.argv.indexOf("-p") >= 0;
console.log("Bundling for " + (isProduction ? "production" : "development") + "...");

module.exports = {
    entry: CONFIG.entry,
    output: {
        path: resolve('./dist'),
        //publicPath: "/js",
        filename: "[name].js"
    },
    mode: isProduction ? "production" : "development",
    //devtool: isProduction ? undefined : "source-map",
    resolve: {
        symlinks: false
    },
    optimization: {
        // Split the code coming from npm packages into a different file.
        // 3rd party dependencies change less often, let the browser cache them.
        splitChunks: {
            cacheGroups: {
                commons: {
                    test: /node_modules/,
                    name: "vendors",
                    chunks: "all"
                }
            }
        },
        minimizer: isProduction ? [new MinifyPlugin()] : []
    },
    // DEVELOPMENT
    //      - HotModuleReplacementPlugin: Enables hot reloading when code changes without refreshing
    plugins: isProduction ? [] : [
        new webpack.HotModuleReplacementPlugin(),
        new webpack.NamedModulesPlugin()
    ],
    // Configuration for webpack-dev-server
    devServer: {
        proxy: CONFIG.devServerProxy,
        hot: true,
        inline: true,
        historyApiFallback: CONFIG.historyApiFallback,
        contentBase: CONFIG.contentBase
    },
    // - fable-loader: transforms F# into JS
    // - babel-loader: transforms JS to old syntax (compatible with old browsers)
    module: {
        rules: [
            {
                test: /\.fs(x|proj)?$/,
                use: "fable-loader"
            },
            {
                test: /\.js$/,
                exclude: /node_modules/,
                use: {
                    loader: "babel-loader",
                    options: CONFIG.babel
                },
            },
            {
                test: indexHtml,
                use: [
                    // "file-loader",
                    {
                        loader: "file-loader",
                        options: {
                            name: "[name].[ext]"
                        }
                    },
                    "extract-loader",
                    {
                        loader: "html-loader",
                        options: {
                            attrs: [
                                //"img:src",
                                "link:href"
                            ]
                        }
                    }
                ]
            },
            {
                test: /\.css$/,
                use: [
                    "file-loader",
                    "extract-loader",
                    {
                        loader: "css-loader",
                        options: {
                            sourceMap: true
                        }
                    }
                ]
            },
            //{
            //    test: /\.(jpg|png)$/,
            //    use: "file-loader"
            //}
        ]
    }
};

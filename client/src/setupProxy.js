const { createProxyMiddleware } = require("http-proxy-middleware");

module.exports =  function(app){
    app.use("/api/v1/books/GetBookList",
        createProxyMiddleware({
            target: "http://localhost:57356",
            changeOrigin: true
        }

        )
    );
};
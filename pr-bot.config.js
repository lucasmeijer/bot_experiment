module.exports = {
    botUsername: `lucasmeijer`,
    repoDetails: {
      owner: "lucasmeijer",
      repo: "bot_experiment"
    },
    plugins: [
      new prbot.plugins.Size({
        globPattern: '**/*.js',
        globOptions: {
          ignore: [
            '**/node_modules/**/*',
          ]
        },
      }),
    ],
  };
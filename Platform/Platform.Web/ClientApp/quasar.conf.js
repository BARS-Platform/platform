const path = require('path')

function extendWebpackAliases(cfg) {
  cfg.resolve.alias['~'] = __dirname
  cfg.resolve.alias['@'] = path.resolve(__dirname, 'src')
}

function extendWebpackTypescript(cfg) {
  cfg.resolve.extensions.push('.ts')

  cfg.module.rules.push({
    test: /\.(tsx?)$/,
    loader: 'ts-loader',
    options: { appendTsSuffixTo: [/\.vue$/] }
  })
}

function extendWebpackPug(cfg) {
  cfg.module.rules.push({
    test: /\.pug$/,
    loader: 'pug-plain-loader'
  })
}

module.exports = function(ctx) {
  return {
    boot: ['vuelidate'],

    css: ['app.styl'],

    extras: [
      // 'ionicons-v4',
      // 'mdi-v3',
      // 'fontawesome-v5',
      // 'eva-icons',
      // 'themify',
      // 'roboto-font-latin-ext', // this or either 'roboto-font', NEVER both!

      'roboto-font', // optional, you are not bound to it
      'material-icons' // optional, you are not bound to it
    ],

    framework: {
      // iconSet: 'ionicons-v4',
      // lang: 'de', // Quasar language

      // all: true, // --- includes everything; for dev only!

      components: [
        'QLayout',
        'QHeader',
        'QDrawer',
        'QPageContainer',
        'QPage',
        'QToolbar',
        'QToolbarTitle',
        'QBtn',
        'QIcon',
        'QList',
        'QItem',
        'QItemSection',
        'QItemLabel',
        'QTable',
        'QTh',
        'QTr',
        'QTd',
        'QCard',
        'QCardSection',
        'QCardActions',
        'QInput',
        'QSpace',
        'QForm'
      ],

      cssAddon: true,

      directives: ['Ripple'],

      plugins: ['Notify', 'Cookies', 'Loading'],

      config: {
        loading: {}
      }
    },

    supportIE: false,

    build: {
      scopeHoisting: true,
      distDir: './dist', // used by dotnet project
      vueRouterMode: 'history',
      // vueCompiler: true,
      // gzip: true,
      // analyze: true,
      extractCSS: true,
      extendWebpack(cfg) {
        extendWebpackAliases(cfg)
        extendWebpackTypescript(cfg)
        extendWebpackPug(cfg)
      },
      forceDevPublicPath: true
    },

    devServer: {
      // https: true,
      // port: 8080,
      open: true // opens browser window automatically
    },

    animations: [],

    ssr: {
      pwa: false
    }
  }
}

import starlight from '@astrojs/starlight';
import { defineConfig } from 'astro/config';

const isProd = process.env.NODE_ENV === 'production';

// https://astro.build/config
export default defineConfig({
  site: 'https://mitchhay.github.io/StageZero/',
  // As per Astro documentation, set base as the GitHub repo name (if prod deploy)
  // Ref: https://docs.astro.build/en/guides/deploy/github/
  base: isProd 
    ? '/StageZero' 
    : '',
	integrations: [
		starlight({
			title: 'StageZero',
      logo: {
        src: './src/assets/stagezero_logo.png'
      },
      defaultLocale: 'en',
			social: [
        { icon: 'github', label: 'GitHub', href: 'https://github.com/mitchHay/StageZero' },
      ],
      editLink: {
        baseUrl: 'https://github.com/mitchHay/StageZero/edit/main/docs/',
      },
			sidebar: [
				{
					label: 'Guides',
					autogenerate: { directory: 'guides' }
				},
				{
					label: 'Reference',
					autogenerate: { directory: 'reference' },
				},
			],
      customCss: [
				'./src/styles/custom.scss',
			],
      components: {
        Head: './src/components/head/Head.astro',
      },
		}),
	],
});

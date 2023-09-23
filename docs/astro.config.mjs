import { defineConfig } from 'astro/config';
import starlight from '@astrojs/starlight';

const isProd = process.env.NODE_ENV === 'production';

// https://astro.build/config
export default defineConfig({
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
			social: {
				github: 'https://github.com/mitchHay/StageZero',
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
		}),
	],
});

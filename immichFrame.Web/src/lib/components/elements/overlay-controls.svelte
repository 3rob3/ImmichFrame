<script lang="ts">
	import { createEventDispatcher } from 'svelte';
	import { mdiChevronRight, mdiPlay, mdiPause } from '@mdi/js';
	import Icon from './icon.svelte';
	import { ProgressBarStatus } from './progress-bar.svelte';

	const dispatch = createEventDispatcher();

	export let status: ProgressBarStatus;

	function clickNext() {
		dispatch('next');
	}
	function clickBack() {
		// dispatch('back');
	}
	function clickPause() {
		dispatch('pause');
	}
	function clickSettings() {
		// dispatch('settings');
	}
</script>

<div class="absolute h-full w-full top-0 left-0 z-[100] grid grid-cols-3 gap-2">
	<div class="group text-center content-center">
		<button class="opacity-0 group-hover:opacity-100 text-primary" on:click={clickBack}> </button>
	</div>

	<div class="grid grid-rows-3">
		<div class="group text-center content-center">
			<button
				class="justify-center items-center opacity-0 hover:opacity-100 text-primary"
				on:click={clickSettings}
			>
			</button>
		</div>

		<div class="group text-center content-center">
			<button on:click={clickPause} class="opacity-0 group-hover:opacity-100 text-primary">
				<Icon
					title={status == ProgressBarStatus.Paused ? 'Play' : 'Pause'}
					path={status == ProgressBarStatus.Paused ? mdiPlay : mdiPause}
					size="33vh"
				/>
			</button>
		</div>

		<div class="group text-center content-center">
			<button class="justify-center items-center opacity-0 hover:opacity-100 text-primary">
			</button>
		</div>
	</div>

	<div class="group text-center content-center">
		<button class="opacity-0 group-hover:opacity-100 text-primary" on:click={clickNext}
			><Icon title="Next" path={mdiChevronRight} size="33vh" />
		</button>
	</div>
</div>

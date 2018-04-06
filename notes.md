Notes
===

`npx tsc --init` created the tsconfig.json
* What is npx?
 * [npx](https://github.com/zkat/npx) is an utility that either executes the command and if necessary, installs it prior to execution.

`npm install @types/node --save-dev` allowed me to use node modules, e.g. `fs`
* How do I use other modules, e.g. `XMLDocument` and `XMLSerializer`?
 * Looks like `@types` come from [DefinitelyTyped](http://definitelytyped.org/)
 * What about the types for default Web APIs?
  * They are built in, and they would work in the browser environment. They don't work in node, outside of the browser.
  * Consider using something like [parse5](https://github.com/inikulin/parse5) and [xmlserializer](https://www.npmjs.com/package/xmlserializer)

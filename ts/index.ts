import { SvgBuilder } from "./svgBuilder";

let svg = new SvgBuilder()
svg.raw("sample text")
svg.save("C:/src/laservg/out/sample.svg");

import fs = require('fs');

export class SvgBuilder {
    body: string = "";

    save(fileName: string): void {
        console.log(this.body, fileName);

        fs.writeFile(fileName, this.body, function(err) {
            if(err) {
                return console.log(err);
            }

            console.log("The file was saved!");
        });
    }

    enterText(text: string): void {
        this.body = text;
    }
}

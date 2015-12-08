FOR /L %%A IN (1,1,20) DO (
rd /s /q C:\Images\Now\Pos%%A
)
net use z: \\VANADIUM\Images
FOR /L %%A IN (1,1,20) DO (
rd /s /q z:\Now\Pos%%A
)